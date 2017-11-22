const path = require('path');

module.exports = {
  devtool: 'source-map',
  entry: {
    app:  path.resolve(__dirname, 'src', 'index.js'),
  },
  output: {
    filename: '[name].js',
    path: path.resolve(__dirname, '..', 'Roomie.Web.Website'),
  },
  module: {  
    rules: [
      {
        test: /\.js$/,
        exclude: /node_modules/,
        use: {
          loader: 'babel-loader',
          options: {
            presets: ['babel-preset-env']
          },
        },
      },
    ],
  },
};
