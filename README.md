# spready

A spreadsheet you can put into a version control system and diff the same as any other text based language. You can compile the spreadsheet language into a regular spreadsheet.

```
spready init "my spreadsheet"
```

Edit the spreadsheet in a suitable text editor.

```
spready compile "my spreadsheet"
```

The idea eventually is to split the look of the spreadsheet from the logic of the spreadsheet. So you can have the same spreadsheet but have it look different by applying a different style to it when it is compiled.

I'd also like to examine how unit testing of the spreadsheet could be facilitated.
