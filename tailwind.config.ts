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
      },
      animation: {
        'slide-down': 'slide-down 500ms',
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
      },
    },
  },
  plugins: [],
} satisfies Config;
