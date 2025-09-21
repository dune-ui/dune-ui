const path = require('path');
const MiniCssExtractPlugin = require('mini-css-extract-plugin');
const CopyPlugin = require('copy-webpack-plugin');

module.exports = [
    // -----------------------
    // CSS
    // ------    
    {
        name: 'css',
        entry: './src/styles.css', // Your main CSS entry file
        output: {
            path: path.resolve(__dirname, '..', 'wwwroot'),
            filename: 'css-dummy-bundle.js',
        },
        module: {
            rules: [
                {
                    test: /\.css$/,
                    use: [
                        MiniCssExtractPlugin.loader,
                        'css-loader',
                        'postcss-loader',
                    ],
                },
            ],
        },
        plugins: [
            new MiniCssExtractPlugin({
                filename: 'styles.css', // The name of your output CSS file
            })
        ],
    },
    // -----------------------
    // Lucide Icons
    // ------
    {
        name: 'icons',
        entry: {},
        output: {
            path: path.resolve(__dirname, '..'),
            filename: 'lucide-icons-dummy-bundle.js', // This can be anything; it will be an empty JS file
        },
        plugins: [
            new CopyPlugin({
                patterns: [
                    {
                        from: 'node_modules/lucide-static/icon-nodes.json'
                    },
                ],
            }),
        ],
    }
];