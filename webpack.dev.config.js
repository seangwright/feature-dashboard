const sharedConfig = require('./webpack.shared.config');

const config = sharedConfig.getConfig(false);

module.exports = config;
