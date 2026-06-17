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

# Lint (ESLint on JS output)
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

`src/App/*.fs` → Fable (F#→JS) → `src/App/*.fs.js` → webpack → `public/bundle.js`

Fable compiles each `.fs` file to a sibling `.fs.js`. Webpack entry is `src/App/App.fs.js`. The `.fs.js` files are committed (checked in) as Fable output artifacts.

### Runtime: Cloudflare Workers + Assets

`wrangler.jsonc` configures a Worker that serves `./public` as static assets. Auth is Cloudflare Access (`/.auth/me`, `/.auth/login/:provider`, `/.auth/logout`).

### F# app structure (`src/App/`)

- **`App.fs`** — entry point: loads stylesheets, sets title, hash-based router, mounts Sutil app
- **`Navigable.fs`** — hash-based routing via `window.location.hash`; `bindHash` maps hash → view; `nav` sets hash
- **`Auth.fs`** — Elmish store wrapping `/.auth/me`; exposes `model`, `dispatch`, `principal`, and active patterns `Loading|LoggedOut|Principal`
- **`Components.fs`** — shared UI primitives (`page` wrapper)
- **`pages/`** — one module per route: `Hero`, `Login`, `Music`, `Artists`, `Cannes`, `Wishlist`

UI is built with **Sutil** (reactive Elmish-style for browser, no React). Styling is **Tailwind v4** utility classes inline on `Html.*c` helpers (the `c` suffix takes a class string).

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
