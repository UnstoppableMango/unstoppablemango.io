// Note this only includes basic configuration for development mode.
// For a more comprehensive configuration check:
// https://github.com/fable-compiler/webpack-config-template

var path = require('path');

module.exports = {
  mode: 'development',
  entry: {
    bundle: './src/Landing/Landing.fs.js',
    'old.bundle': './src/App/App.fs.js',
  },
  output: {
    path: path.join(__dirname, './public'),
    filename: '[name].js',
  },
  devServer: {
    static: {
      directory: path.join(__dirname, 'public'),
    },
    port: 8080,
    historyApiFallback: {
      rewrites: [
        { from: /^\/old/, to: '/old/index.html' },
      ],
    },
  },
};
