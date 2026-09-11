# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## What this is

Personal website (unstoppablemango.io). F# compiled to JS via Fable, styled with Tailwind, deployed as a Cloudflare Worker. Infrastructure managed with Pulumi.

## Commands

```bash
# Install deps (also runs dotnet tool restore)
npm ci

# Dev server (Fable watch + webpack dev server on :8080)
npm start

# Dev server + Tailwind watch + wrangler dev (full stack)
npm run start:all

# Full production build (Fable → JS → webpack + tailwind minify)
npm run build

# Lint (ESLint; app/public output is ignored by config)
npm run lint

# Tests (compile test/App.Tests with Fable, run with mocha)
npm test

# Format F# code
fantomas <file>

# Infrastructure preview/deploy (from infra/ dir)
make infra         # pulumi up --cwd infra
```

**F# formatting:** use `fantomas` (dotnet tool, installed via `dotnet tool restore`).

**No single-file test run** — `npm test` recompiles all tests then runs mocha on `dist/tests`.

## Architecture

### Build pipeline

`src/App/*.fs` → Fable (F#→JS) → `src/App/*.fs.js` (generated) → webpack → `public/bundle.js`

Fable compiles each `.fs` file to a sibling `.fs.js`. Webpack entry is `src/App/App.fs.js`. The `.fs.js` files are generated during `dotnet fable` and are gitignored (see `*.fs.js` in `.gitignore`).

### Reading generated output

Two codegen layers sit between the source and what ships: Fable compiles `.fs` to `.fs.js`, and Tailwind compiles utility class strings to CSS.
Verify any claim about the generated JavaScript or CSS against the build output rather than the source text.

- **F# interpolated strings are `PrintfFormat`.** A literal percent is written `%%`. `$"width: {pct}%%"` compiles to `` `width: ${pct}%` ``; a single `%` does not survive.
- **Tailwind normalizes math in arbitrary values.** `calc(100%-9px)` emits `calc(100% - 9px)`, with the whitespace the CSS spec requires around the binary operator. Spelling it `calc(100%_-_9px)` emits the same declaration; only the escaped class selector differs.

To check: `dotnet fable src/App` then read `src/App/**/*.fs.js`, or `npm run tailwind:prod` then read `public/tailwind.css`.

### Runtime: Cloudflare Workers + Assets

`wrangler.jsonc` configures a Worker that serves `./public` as static assets.

### F# app structure (`src/App/`)

- **`App.fs`** — entry point: loads stylesheets, sets title, hash-based router, mounts Sutil app
- **`Navigable.fs`** — hash-based routing via `window.location.hash`; `bindHash` maps hash → view; `nav` sets hash
- **`Components.fs`** — shared UI primitives (`page` wrapper)
- **`pages/`** — one module per route: `Hero`, `Music`, `Artists`, `Cannes`, `Wishlist`

UI is built with **Sutil** (reactive Elmish-style for browser, no React). Styling is **Tailwind v4** utility classes inline on `Html.*c` helpers (the `c` suffix takes a class string).

### Styling: Pulp (`src/pulp/`)

Pulp is the design system behind the v2 site. There is no `tailwind.config.ts`; the theme lives in CSS.

- **`src/app.css`** — the entry point. `@import "tailwindcss" source(none)` then `@source "./App/**/*.fs.js"`. Automatic content detection is off on purpose: it respects `.gitignore`, which excludes the compiled `.fs.js` files Tailwind has to scan, and it otherwise generates utilities out of prose in Markdown files. An explicit `@source` glob is exempt from the gitignore filter. A new source directory outside `src/App` needs the glob widened or it produces no CSS.
- **`src/pulp/palettes.css`** — one class-scoped block per palette (`.pulp-theme-slate` and friends), plus a shared block deriving the glows with `color-mix`. Slate also sits on `:root` as the default. Adding a palette means editing both the block and the derived selector list; a derived value declared only on `:root` freezes against the default and inherits that way into every other palette.
- **`src/pulp/surfaces.css`** — everything palette independent: status colours, the `fg` type ramp, and the `glass` material family where white tints lift a surface and black tints cut it in.
- **`src/pulp/intensity.css`** — the second axis, `.pulp-intensity-clean` and `.pulp-intensity-readable`, which override what the two files above declare. It has to be imported after them, since the classes share the root element and carry the same specificity. Everything here is unlayered while Tailwind's utilities sit in a cascade layer, which is what lets a rule here stop an `animate-*` utility without `!important`. `hud` has no block: it is the baseline.
- **`src/pulp/tokens.css`** — maps the `--pulp-*` variables onto Tailwind tokens. These blocks must be `@theme inline`: a plain `@theme` emits the token on `:root`, where it resolves once against the default palette and inherits frozen, which silently breaks palette switching. `h-screen`, `min-h-screen` and `bg-v2-glass` have no v4 theme namespace and are written as `@utility` rules.
- **`src/App/Pulp/Theme.fs`** — owns both axis choices, palette and intensity: a Sutil store each, `localStorage` persistence, and one class swap that removes only its own prefix, since the two axes share the root element. Intensity falls back to `prefers-contrast` and `prefers-reduced-motion` when storage holds nothing. A synchronous script in `public/index.html` applies both classes before the bundle loads to prevent a flash; it duplicates the storage keys, the class prefixes and the preference fallback because it runs before any module exists.

### Infrastructure (`infra/`)

Pulumi TypeScript stack. Deploys:
- Cloudflare Worker (`@pulumi/cloudflare`)
- Worker assets via `wrangler deploy` (triggered by file hash changes)
- Custom domains + SSL settings per zone

Config in `infra/Pulumi.prod.yaml`. Requires `PULUMI_ACCESS_TOKEN` and Cloudflare API token.

### CI (`.github/workflows/ci.yml`)

1. `build` job: `npm ci` → `npm run build` → `npm run lint` → upload `public/` artifact
2. `infra` job: downloads artifact, runs `pulumi preview` (PR) or `pulumi up` (main push)
3. `nix` job: `nix flake check`

### Nix dev environment

`flake.nix` provides `devShells.default` with all tooling (dotnet SDK 10, fable, fantomas, node, pulumi, tailwind, webpack-cli, etc.). Use `nix develop` or direnv (`.envrc` present).
