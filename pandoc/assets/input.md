# Lorem Ipsum

Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed do eiusmod tempor 
incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis
 nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.

## Introduction

::: {typst:text:style=\"italic\"}
Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vestibulum at justo non
lectus malesuada posuere. Praesent sed nunc sit amet nisi tincidunt convallis. 
Integer porttitor, lectus eget suscipit tincidunt, nisl risus luctus neque, eget
feugiat lorem erat at erat.
:::

Lorem [ipsum dolor sit amet, consectetur]{typst:text:fill="rgb(\"ff0000\")"} 
adipiscing elit. Curabitur pulvinar, nunc at gravida varius, ligula risus 
hendrerit massa, vitae luctus sem magna necerat. Pellentesque habitant morbi 
tristique senectus et netus et malesuada fames ac turpis egestas.

Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer eget turpis 
vitae tortor vestibulum fermentum. Suspendisse potenti. Donec elementum ipsum ac
massa commodo, quis tempor sem vulputate.

## Lists

### Bullet List

- Lorem ipsum dolor sit amet
- Consectetur adipiscing elit
- Sed do eiusmod tempor incididunt
- Ut labore et dolore magna aliqua
- Ut enim ad minim veniam
- Quis nostrud exercitation ullamco laboris
- Nisi ut aliquip ex ea commodo consequat

### Numbered List

1. Lorem ipsum dolor sit amet.
2. Consectetur adipiscing elit.
3. Sed do eiusmod tempor incididunt.
4. Ut labore et dolore magna aliqua.
5. Ut enim ad minim veniam.
6. Quis nostrud exercitation ullamco laboris.
7. Nisi ut aliquip ex ea commodo consequat.

## Another Section

Lorem ipsum dolor sit amet, consectetur adipiscing elit. Praesent ultricies
risus sit amet lectus feugiat, sit amet pharetra nisl vulputate. Suspendisse
potenti.

Lorem ipsum dolor sit amet, consectetur adipiscing elit. Cras a odio vitae orci
interdum scelerisque. Nulla facilisi. Sed cursus nibh in velit feugiat, sed
dictum lectus tincidunt.

Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vivamus faucibus turpis
eget est vestibulum, vel tempus risus scelerisque. Morbi malesuada diam sit amet
dui aliquet, vitae suscipit libero viverra.

Lorem ipsum dolor sit amet, consectetur adipiscing elit. Proin vitae lectus id
libero volutpat dignissim. Aliquam erat volutpat. Suspendisse vitae elit at odio
ultrices tincidunt.

Lorem ipsum dolor sit amet, consectetur adipiscing elit. Pellentesque habitant
morbi tristique senectus et netus et malesuada fames ac turpis egestas.

Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed consequat, metus eu
dignissim cursus, justo nunc vulputate massa, a aliquam justo erat vel purus.

Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer feugiat, velit
quis egestas luctus, risus magna tincidunt est, vel consequat purus neque eget
eros.

## Sample Table

| Header 1 | Header 2 | Header 3 | Header 4 |
|----------|----------|----------|----------|
| Cell 1,1 | Cell 1,2 | Cell 1,3 | Cell 1,4 |
| Cell 2,1 | Cell 2,2 | Cell 2,3 | Cell 2,4 |
| Cell 3,1 | Cell 3,2 | Cell 3,3 | Cell 3,4 |
| Cell 4,1 | Cell 4,2 | Cell 4,3 | Cell 4,4 |

## Large Table

| Header 1  | Header 2  | Header 3  | Header 4  | Header 5  | Header 6  |
|---------- |---------- |---------- |---------- |---------- |---------- |
| Cell 1,1  | Cell 1,2  | Cell 1,3  | Cell 1,4  | Cell 1,5  | Cell 1,6  |
| Cell 2,1  | Cell 2,2  | Cell 2,3  | Cell 2,4  | Cell 2,5  | Cell 2,6  |
| Cell 3,1  | Cell 3,2  | Cell 3,3  | Cell 3,4  | Cell 3,5  | Cell 3,6  |
| Cell 4,1  | Cell 4,2  | Cell 4,3  | Cell 4,4  | Cell 4,5  | Cell 4,6  |
| Cell 5,1  | Cell 5,2  | Cell 5,3  | Cell 5,4  | Cell 5,5  | Cell 5,6  |
| Cell 6,1  | Cell 6,2  | Cell 6,3  | Cell 6,4  | Cell 6,5  | Cell 6,6  |
| Cell 7,1  | Cell 7,2  | Cell 7,3  | Cell 7,4  | Cell 7,5  | Cell 7,6  |
| Cell 8,1  | Cell 8,2  | Cell 8,3  | Cell 8,4  | Cell 8,5  | Cell 8,6  |
| Cell 9,1  | Cell 9,2  | Cell 9,3  | Cell 9,4  | Cell 9,5  | Cell 9,6  |
| Cell 10,1 | Cell 10,2 | Cell 10,3 | Cell 10,4 | Cell 10,5 | Cell 10,6 |

## Raw Typst Table

```{=typst}
#table(
  columns: (auto, auto, 1fr, 1fr),
  align: (left, left, center, right),
  stroke: 0.5pt + black,
  
  table.header(
    [Header 1], [Header 2], [Header 3], [Header 4]
  ),

  [cell1,1], [cell1,2], [cell1,3], [cell1,4],
  [cell2,1], [cell2,2], [cell2,3], [cell2,4],
  [cell3,1], [cell3,2], [cell3,3], [cell3,4],
  [cell4,1], [cell4,2], [cell4,3], [cell4,4],
  [cell5,1], [cell5,2], [cell5,3], [cell5,4],
  [cell6,1], [cell6,2], [cell6,3], [cell6,4],
  [cell7,1], [cell7,2], [cell7,3], [cell7,4],
  [cell8,1], [cell8,2], [cell8,3], [cell8,4],
  [cell9,1], [cell9,2], [cell9,3], [cell9,4],
  [cell10,1], [cell10,2], [cell10,3], [cell10,4],
  [cell11,1], [cell11,2], [cell11,3], [cell11,4],
  [cell12,1], [cell12,2], [cell12,3], [cell12,4],
  [cell13,1], [cell13,2], [cell13,3], [cell13,4],
  [cell14,1], [cell14,2], [cell14,3], [cell14,4],
  [cell15,1], [cell15,2], [cell15,3], [cell15,4],
)
```

## Block Quote

> Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed do eiusmod tempor
> incididunt ut labore et dolore magna aliqua.
>
> Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut
> aliquip ex ea commodo consequat.
>
> Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore
> eu fugiat nulla pariatur.

## Code Example

```js
function printMessage() {
    console.log("Lorem ipsum dolor sit amet, consectetur adipiscing elit");
}

printMessage();
```

## Long Section

Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nulla facilisi. Fusce
vulputate est id lorem tristique, a feugiat nisi tincidunt. Mauris nec dolor vel
nisl facilisis consectetur. Donec feugiat justo et nisi tincidunt, vitae viverra
lorem commodo.

Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec feugiat neque non
purus malesuada, a interdum erat cursus. Morbi suscipit sem at erat volutpat, et
laoreet metus tempus. Suspendisse potenti.

Lorem ipsum dolor sit amet, consectetur adipiscing elit. Quisque posuere lorem 
at augue pretium, non posuere lorem volutpat. Donec efficitur arcu sed lectus
facilisis, et malesuada nisl posuere.

Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nam vitae augue non
nunc malesuada tristique. Morbi posuere metus et justo luctus, ac pulvinar
libero vulputate. Sed pretium magna vel massa feugiat cursus.

Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer blandit nisl
sed felis tempor, quis bibendum ipsum tincidunt. Sed vel magna vel justo
consequat malesuada.

Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vestibulum tincidunt,
mauris non dignissim accumsan, libero massa tempor libero, in faucibus metus
lacus ac erat. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices
posuere cubilia curae.

Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed vehicula velit sit
amet magna interdum, a pretium ligula efficitur. Cras non libero sed urna
tincidunt suscipit.

Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec condimentum velit
non odio efficitur, in tincidunt augue dignissim. Integer consequat, lectus sit
amet convallis luctus, nunc sapien malesuada mauris, sit amet interdum lorem
urna ut magna.

## Conclusion

Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed do eiusmod tempor
incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis
nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.

Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu
fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in
culpa qui officia deserunt mollit anim id est laborum.