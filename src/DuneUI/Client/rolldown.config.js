import { defineConfig } from 'rolldown';

export default defineConfig({
    input: 'js/dune-ui.js',
    output: {
        file: '../wwwroot/dune-ui.js',
        format: 'iife',
        name: 'DuneUI',
        minify: true,
        sourcemap: true,
    },
    platform: 'browser',
});