1. Copy file from https://github.com/shadcn-ui/ui/tree/main/apps/v4/registry/styles
2. Rename to <theme name>.themepack
3. Remove root containing style
4. Run the following regex find and replace to restructure the file into the format which
can be understood by the source generator.
Find: \.cn-(.*)\ \{\n\@apply\ (.*)\;\n\}
Replace: -dui-$1\n$2

5. Run the following regex to add radio-group support to field group. We are just making 
a copy of checkbox-group
Find: -dui-field-group\n(.*data-\[slot=checkbox-group\]:)(\S+)
Replace: -dui-field-group\n$1$2\ data-[slot=radio-group]:$2

6. Run the following regex to remove the ring around inputs that are in an error state.
The ring is confusion as it is the same ring used when an input has focus, so the presence
of this ring makes it difficult to see when an input that is in error state has input focus
Find: \saria-invalid:ring-(\[)?[1-9](px\])?
Replace: <empty string>

7. Run the following regex to create copies of aria-invalid styles used to indicate an error
to work with the ASP.NET Core validation class (.input-validation-error) 
Find: (\s)(dark:)?(aria\-invalid:)(\S+)
Replace: $1$2$3$4$1$2[&.input-validation-error]:$4