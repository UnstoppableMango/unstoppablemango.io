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
        jet: '#2e2d2e',
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
        'cool-gray': {
          50: '#8d8d9f',
          100: '#7a7a8a',
          200: '#676775',
          300: '#555561',
          400: '#44444d',
          500: '#33333b',
          600: '#232329',
          700: '#141418',
          800: '#070709',
          900: '#010101',
        },
        'cyber-pink': {
          DEFAULT: '#ff2d78',
          dim: '#cc1a55',
          glow: '#ff2d7844',
          subtle: '#ff2d7811',
        },
      },
      animation: {
        'slide-down': 'slide-down 500ms',
        'glitch': 'glitch 600ms steps(2) infinite',
        'glitch-clip': 'glitch-clip 800ms steps(2) infinite',
        'scan-line': 'scan-line 3s linear infinite',
        'flicker': 'flicker 4s steps(1) infinite',
        'power-on': 'power-on 400ms ease-out forwards',
        'hud-appear': 'hud-appear 300ms ease-out forwards',
        'pulse-pink': 'pulse-pink 2s ease-in-out infinite',
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
        'pulse-pink': {
          '0%, 100%': { boxShadow: '0 0 4px #ff2d78, 0 0 8px #ff2d78' },
          '50%': { boxShadow: '0 0 12px #ff2d78, 0 0 24px #ff2d7844' },
        },
      },
    },
  },
  plugins: [],
} satisfies Config;
