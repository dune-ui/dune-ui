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
        main: "./src/stellar-ui.ts",
        "stellar-ui": "./src/stellar-ui.css",
      },
      output: {
        entryFileNames: "stellar-ui.js",
        assetFileNames: (assetInfo) => {
          return "[name].[ext]";
        },
      },
    },
  },
});
