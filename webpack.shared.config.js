const path = require('path');
const glob = require('glob');

const CleanWebpackPlugin = require('clean-webpack-plugin');
const ExtractTextPlugin = require('extract-text-webpack-plugin');
const UglifyJsPlugin = require('uglifyjs-webpack-plugin');
const webpack = require('webpack');

// Copied from angular-cli so we have nice stats / debugging
const stats = {
  colors: true,
  hash: true,
  timings: true,
  chunks: true,
  chunkModules: false,
  children: false,
  modules: false,
  reasons: false,
  warnings: true,
  errors: true,
  assets: true,
  version: false,
  errorDetails: false,
  moduleTrace: false
};

const pathsToClean = ['dist'];

const cleanOptions = {
  root: path.join(__dirname, 'FeatureDashboard.Web', 'wwwroot')
};

/**
 * Builds the plugins array which depends on whether or not isProduction is true
 */
const getPlugins = isProduction => {
  const plugins = [
    new CleanWebpackPlugin(pathsToClean, cleanOptions),
    new ExtractTextPlugin({ filename: 'app.bundle.css' }),
    new webpack.ProvidePlugin({
      $: 'jquery',
      jQuery: 'jquery'
    })
  ];

  // Only uglify in production
  if (isProduction) {
    plugins.push(new UglifyJsPlugin());
  }

  return plugins;
};

/**
 * Builds a webpack config object that is toggled based on whether or not isProduction is true
 */
const getConfig = isProduction => ({
  watch: !isProduction,
  watchOptions: isProduction
    ? undefined
    : {
        aggregateTimeout: 300,
        poll: 1000,
        ignored: /node_modules/
      },
  mode: isProduction ? 'production' : 'development',
  stats,
  devtool: isProduction ? undefined : 'inline-source-map',
  entry: ['./FeatureDashboard.Web/app.ts'],
  output: {
    filename: 'bundle.js',
    path: path.resolve(__dirname, 'FeatureDashboard.Web/wwwroot/dist')
  },
  resolve: {
    extensions: ['.ts', '.js'],

    // Allows font tree shaking (currently not working)
    alias: {
      '@fortawesome/fontawesome-pro-solid$':
        '@fortawesome/fontawesome-pro-solid/shakable.es.js'
    }
  },
  module: {
    rules: [
      {
        test: /\.ts/,
        exclude: /node_modules/,

        // Use babel for transpiling and polyfilling after converting the Typescript to es2015
        use: [{ loader: 'babel-loader' }, { loader: 'ts-loader' }]
      },
      {
        test: /\.(s*)css$/,
        use: ExtractTextPlugin.extract({
          fallback: 'style-loader',
          use: [
            { loader: 'css-loader', options: { sourceMap: !isProduction } },
            { loader: 'postcss-loader', options: { sourceMap: !isProduction } },
            { loader: 'sass-loader', options: { sourceMap: !isProduction } }
          ]
        })
      }
    ]
  },
  plugins: getPlugins(isProduction)
});

module.exports = {
  getConfig
};
