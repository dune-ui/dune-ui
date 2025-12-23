import { defineConfig } from "vite";
import tailwindcss from "@tailwindcss/vite";
import { viteStaticCopy } from "vite-plugin-static-copy";

export default defineConfig({
  plugins: [
    tailwindcss(),
    viteStaticCopy({
      targets: [
        {
          src: "node_modules/lucide-static/icon-nodes.json",
          dest: "lucide-static",
        },
      ],
    }),
  ],
  build: {
    outDir: "dist",
    rollupOptions: {
      input: {
        main: "./src/dune-ui.ts",
        "dune-ui": "./src/dune-ui.css",
      },
      output: {
        entryFileNames: "dune-ui.js",
        assetFileNames: (assetInfo) => {
          return "[name].[ext]";
        },
      },
    },
  },
});
