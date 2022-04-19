4/19/2022

**LayoutBugs Project demonstrates Layout erros in Microsoft.WindowsAppSDK**

- **ListView**
- **ItemsRepeater**

by showing four ways of listing items at the same time using the same ObservableCollection of Items and the same DataTemplate for layout:

- an ItemsRepeater using the default (vertical) layout embedded in a ScrollView
- an ItemsRepeater set to use a horizontal layout embedded in a ScrollView
- a ListView using the default (vertical) layout
- a ListView set to use a horizontal layout

The DataTemplate are alligned according to their names:

- all VerticalAllignment="Top"
- HorizontalAllignment="Left" or HorizontalAllignment="Stretch"

An Item object has

- a Number as a sequence number when created
- a random count of Number so that the text displayed (VerticalLinesText, HorizontalLinesText) will be of a different length. 
- VerticalLinesText: "Item #(Number):\nX\nX-1\n...0"
- HorizontalLinesText: "Item #(Number): X X-1 ...0"

The Item presentation within the DataTemplate consist either of

- a TextBlock with a fixed Text "- X -"
- a TextBlock showing Item.VerticalLinesText of a random height AND sometimes slightly different number width
- a TextBlock showing Item.HorizontalLinesText of a random width but with the same height 

The Textblock has a "AliceBlue" background and a "DarkBlue" 1 pixel thick border. The "StretchAllignGrid" DataTemplates enclose this in a Grid with a "Aqua" background so the remaing space (which should be consumed by the "Stretch") should be visible. 


**Inputs:**

- ***"Items Count:"*** the number of Item instances within the Items collection to be generated. Changing the number will clear the current Items ObservableCollection and add new Item instances.
- ***"Max. Numbers per Item:"*** the maximum count of Numbers in a Items Lines (thus affecting the Items layout width or height). Changing the number will clear the current Items ObservableCollection and add new Item instances.
- ***"DataTemplate"*** sets the DataTemplate used by all four layout Views. 
- ***"Item Actions:"*** a number how many random delete, insert and move Actions should be performed on the Items ObservableCollection
- ***"Perform Actions"*** will apply the Items Actions count of changes
- ***"Reset Scroll Position"*** ChangeView(0,0, null) on ScrollView of both ItemRepeater, ScrollIntoView(Item[0]) on both ListView
- ***"Enforce ListView.UpdateLayout()"*** if checked, UpdateLayout() will be called on the ListView 
  + before "Reset Scroll Position"
  + after "Items Count:", "Max. Numbers per Item:", "DataTemplate" changes
  + after "Perform Actions"


**Usage:**

- build and start Application
- first try out the different layouts and with each 
  + sroll around, fast if needed (good excuse to get a LogiTech MX Anywhere with a free spinning scroll wheel :-)
  + check the TextBlock layout
- change the "Items Count" and/or "Max. Numbers per Item" values (50 - 100), try out the different layouts and with each
  + scroll around as above
  + check the TextBlock layout

**Layout is done right if:**

- **all "DarkBlue" borders touch in the ItemRepeater views** (e.g. a TWO pixel width line is shown). There should be NO space between them.
- **spacing and layout between the ItemRepeater and ListView is the same sans the Margin added by the ListView** for the selection stuff etc.


**What to look out for:**

- **Top or left misalligned Text**
- **some pixel width "Aqua" background color in the Horiontal ListView** 
- **non touching "DarkBlue" borders in the ItemRepeater views**

**Findings (Microsoft.WindowsAppSDK 1.0.0)**
- only the "TopAllignedTextBlockFixedText" is shown correctly ALL four ways on EVERY "Items Count" value
- all other DataTemplates will be shown differently between the ItemsRepeater and ScrollView if the Layout needs to be scrolled
- there will be layout errors where items
  + are not attached to a side
  + have the wrong spacing between them
- **the Horizontal ListView will NEVER display items with a different height top alligned - its always centered.**

