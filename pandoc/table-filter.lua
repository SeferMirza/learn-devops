function Table(tbl)
  tbl.attr.classes:insert("typst:no-figure")
  tbl.attr.attributes["typst:stroke"] = "0.5pt + black"

  return tbl
end