const sharedConfig = require('./webpack.shared.config');

const config = sharedConfig.getConfig(true);

module.exports = config;
