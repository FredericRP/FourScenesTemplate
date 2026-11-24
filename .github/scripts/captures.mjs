// Runs the WebGL build in headless Chromium (software rendering) and takes screenshots.
// Goes through the four loading scenes, clicks Play on the menu, then clicks on the moving
// flowers of the sample game (found by comparing two frames) to score points.
// Usage: node captures.mjs <build folder> <output folder>
import { chromium } from 'playwright';
import { createServer } from 'node:http';
import { readFile, mkdir } from 'node:fs/promises';
import { join, extname } from 'node:path';

const [root, out] = process.argv.slice(2);
const types = { '.html': 'text/html', '.js': 'application/javascript', '.json': 'application/json', '.wasm': 'application/wasm', '.png': 'image/png', '.css': 'text/css' };

const server = createServer(async (req, res) => {
  let path = decodeURIComponent(new URL(req.url, 'http://x').pathname);
  if (path.endsWith('/')) path += 'index.html';
  try {
    const body = await readFile(join(root, path));
    res.writeHead(200, { 'Content-Type': types[extname(path)] ?? 'application/octet-stream' });
    res.end(body);
  } catch {
    res.writeHead(404).end();
  }
}).listen(8080);

// Keep the WebGL frame readable after presentation, so the page can sample it.
const initScript = () => {
  const getContext = HTMLCanvasElement.prototype.getContext;
  HTMLCanvasElement.prototype.getContext = function (type, attributes) {
    if (type === 'webgl' || type === 'webgl2') attributes = { ...attributes, preserveDrawingBuffer: true };
    return getContext.call(this, type, attributes);
  };
  const probe = document.createElement('canvas');
  probe.width = 96;
  probe.height = 60;
  const grab = () => {
    const ctx = probe.getContext('2d', { willReadFrequently: true });
    ctx.drawImage(document.querySelector('canvas'), 0, 0, probe.width, probe.height);
    return ctx.getImageData(0, 0, probe.width, probe.height).data;
  };
  // Returns the centre (0..1) of the cell that changed the most between two frames, or null.
  window.findMotion = () => new Promise((resolve) => {
    const before = grab();
    setTimeout(() => {
      const after = grab();
      let best = 0, bx = 0, by = 0;
      for (let y = 0; y < probe.height; y++) {
        for (let x = 0; x < probe.width; x++) {
          const i = (y * probe.width + x) * 4;
          const d = Math.abs(after[i] - before[i]) + Math.abs(after[i + 1] - before[i + 1]) + Math.abs(after[i + 2] - before[i + 2]);
          if (d > best) { best = d; bx = x; by = y; }
        }
      }
      window.lastMotion = best;
      resolve(best > 24 ? { x: (bx + 0.5) / probe.width, y: (by + 0.5) / probe.height, d: best } : null);
    }, 120);
  });
};

await mkdir(out, { recursive: true });
const browser = await chromium.launch({ args: ['--use-angle=swiftshader', '--enable-unsafe-swiftshader', '--ignore-gpu-blocklist'] });
const page = await browser.newPage({ viewport: { width: 1024, height: 800 } });
page.on('console', (m) => console.log('[game]', m.text()));
await page.addInitScript(initScript);
await page.goto('http://localhost:8080/');

const canvas = page.locator('canvas').first();
const shot = async (name, wait) => {
  await page.waitForTimeout(wait);
  await canvas.screenshot({ path: join(out, `${name}.png`) });
  console.log('screenshot', name);
};

await shot('01-loading', 12000);
await shot('02-menu', 20000);
// Whole page around the game (header with the project title, footer links).
await page.screenshot({ path: join(out, '00-page.png'), fullPage: true });
// The Play button is anchored at 50 % width, 40 % height from the bottom of the menu.
const box = await canvas.boundingBox();
await canvas.click({ position: { x: box.width * 0.5, y: box.height * 0.6 } });
await shot('03-game', 6000);

// Click on whatever moves (the flowers) for a while, taking a screenshot every few seconds.
let hits = 0;
for (let i = 4; i <= 6; i++) {
  const end = Date.now() + 6000;
  while (Date.now() < end) {
    const target = await page.evaluate(() => window.findMotion());
    if (target) {
      await canvas.click({ position: { x: target.x * box.width, y: target.y * box.height } });
      hits++;
    }
    await page.waitForTimeout(150);
  }
  await shot(`${String(i).padStart(2, '0')}-flowers`, 0);
}
console.log('clicks on moving targets', hits, 'last motion', await page.evaluate(() => window.lastMotion));

await browser.close();
server.close();
