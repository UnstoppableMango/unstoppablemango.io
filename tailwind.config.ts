import type { Config } from 'tailwindcss';

export default {
  content: ['./src/**/*.fs.js'],
  theme: {
    extend: {
      height: {
        screen: '100svh',
      },
      colors: {
        'eerie-black': '#1b1d1e',
        byzantium: {
          50: '#7b2e58',
          100: '#6a274b',
          200: '#5a1f3f',
          300: '#4a1833',
          400: '#3a1228',
          500: '#2c0b1d',
          600: '#1e0612',
          700: '#100209',
          800: '#050103',
          900: '#010000',
        },
        thistle: {
          50: '#dbc5d1',
          100: '#beabb5',
          200: '#a2929b',
          300: '#877981',
          400: '#6d6168',
          500: '#544b50',
          600: '#3c3539',
          700: '#262123',
          800: '#110e10',
          900: '#020202',
        },
        // Pulp. Every entry below reads a `--pulp-*` variable defined under
        // src/pulp: the palette files decide which colour the interface is, and
        // the surfaces file decides how the material behaves.
        primary: {
          DEFAULT: 'var(--pulp-primary)',
          dim: 'var(--pulp-primary-dim)',
          bright: 'var(--pulp-primary-bright)',
          lift: 'var(--pulp-primary-lift)',
        },
        accent: {
          DEFAULT: 'var(--pulp-accent)',
          dim: 'var(--pulp-accent-dim)',
          bright: 'var(--pulp-accent-bright)',
        },
        danger: {
          DEFAULT: 'var(--pulp-danger)',
          dim: 'var(--pulp-danger-dim)',
          bright: 'var(--pulp-danger-bright)',
        },
        warn: 'var(--pulp-warn)',
        ok: 'var(--pulp-ok)',
        // The light itself rather than a material, so it sits outside `glass`.
        lit: 'var(--pulp-lit)',
        // Frosted surfaces over a photo backdrop. Every value is translucent:
        // these tint whatever sits behind them rather than covering it. White
        // tints lift a surface off the backdrop, black tints cut it in.
        glass: {
          fill: 'var(--pulp-glass-fill)', // panels, alerts
          track: 'var(--pulp-glass-track)', // progress rail
          edge: 'var(--pulp-glass-edge)', // borders
          rim: 'var(--pulp-glass-rim)', // inset top light, at rest
          'rim-bright': 'var(--pulp-glass-rim-bright)', // the same rim, lit
          well: 'var(--pulp-glass-well)', // inputs, tags
          press: 'var(--pulp-glass-press)', // hover
          'press-deep': 'var(--pulp-glass-press-deep)', // active
          slab: 'var(--pulp-glass-slab)', // the page level scrim
          cast: 'var(--pulp-glass-cast)', // thrown onto the backdrop
        },
      },
      backgroundImage: {
        // The glass pane itself: a cool diagonal tint with the light falling
        // from the top left.
        'v2-glass': 'linear-gradient(155deg, #e8f2f85c 0%, #9dbccc42 38%, #0d141a1a 100%)',
      },
      boxShadow: {
        'v2-panel': '0 16px 48px #070b1080, inset 0 2px 0 var(--pulp-glass-rim-bright), inset 0 -2px 0 #ffffff1f',
        'v2-inset': 'inset 0 2px 0 #ffffff40',
        'v2-pane': 'inset 0 2px 0 #ffffff66, inset 0 -70px 120px #0a0f1426',
        // The content column, cast onto the backdrop rather than onto glass.
        'v2-column': '0 0 80px var(--pulp-glass-cast)',
      },
      animation: {
        'slide-down': 'slide-down 500ms',
        'glitch': 'glitch 600ms steps(2) infinite',
        'glitch-clip': 'glitch-clip 800ms steps(2) infinite',
        'scan-line': 'scan-line 3s linear infinite',
        'flicker': 'flicker 4s steps(1) infinite',
        'power-on': 'power-on 400ms ease-out forwards',
        'hud-appear': 'hud-appear 300ms ease-out forwards',
        'pulse-primary': 'pulse-primary 2s ease-in-out infinite',
      },
      keyframes: {
        'slide-down': {
          '0%': {
            transform: 'translateY(-50px)',
            opacity: '0',
          },
          '100%': {
            transform: 'translateY(0)',
            opacity: '100',
          },
        },
        'glitch': {
          '0%, 100%': { transform: 'translate(0)', clipPath: 'inset(0 0 0 0)' },
          '20%': { transform: 'translate(-2px, 1px)', clipPath: 'inset(10% 0 80% 0)' },
          '40%': { transform: 'translate(2px, -1px)', clipPath: 'inset(60% 0 20% 0)' },
          '60%': { transform: 'translate(-1px, 2px)', clipPath: 'inset(40% 0 40% 0)' },
          '80%': { transform: 'translate(1px, -2px)', clipPath: 'inset(80% 0 5% 0)' },
        },
        'glitch-clip': {
          '0%, 100%': { transform: 'translate(0)', opacity: '1' },
          '33%': { transform: 'translate(3px, 0)', opacity: '0.8' },
          '66%': { transform: 'translate(-3px, 0)', opacity: '0.9' },
        },
        // Positioned against the nearest positioned ancestor rather than the
        // viewport, so the same sweep works fullscreen and inside a small tile.
        'scan-line': {
          '0%': { top: '-20%' },
          '100%': { top: '120%' },
        },
        'flicker': {
          '0%, 97%, 100%': { opacity: '1' },
          '98%': { opacity: '0.6' },
          '99%': { opacity: '1' },
          '99.5%': { opacity: '0.4' },
        },
        'power-on': {
          '0%': { opacity: '0', transform: 'scaleY(0.02)', filter: 'brightness(3)' },
          '30%': { opacity: '1', transform: 'scaleY(1)', filter: 'brightness(2)' },
          '100%': { opacity: '1', transform: 'scaleY(1)', filter: 'brightness(1)' },
        },
        'hud-appear': {
          '0%': { opacity: '0', transform: 'translateX(-8px)', letterSpacing: '0.5em' },
          '100%': { opacity: '1', transform: 'translateX(0)', letterSpacing: 'inherit' },
        },
        'pulse-primary': {
          '0%, 100%': { boxShadow: '0 0 4px var(--pulp-primary), 0 0 8px var(--pulp-primary)' },
          '50%': { boxShadow: '0 0 12px var(--pulp-primary), 0 0 24px var(--pulp-primary-glow-soft)' },
        },
      },
    },
  },
  plugins: [],
} satisfies Config;
