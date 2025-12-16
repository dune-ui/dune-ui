1. Copy file from https://github.com/shadcn-ui/ui/tree/main/apps/v4/registry/styles
2. Rename to <theme name>.themepack
3. Remove root containing style
4. Run the following regex find and replace
Find: \.cn-(.*)\ \{\n\@apply\ (.*)\;\n\}
Replace: -dui-$1\n$2