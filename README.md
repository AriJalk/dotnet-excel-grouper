# ExcelGrouper

**ExcelGrouper** is a lightweight command-line tool that processes Excel files and assigns group IDs to rows based on numeric similarity in selected columns. It’s useful for fast, rough grouping based on configurable thresholds.

---

## 🛠 How It Works

You prepare:
- An `.xlsx` file
- A `.json` file with the **same name**, in the **same folder**

Example:
```
/your-folder/
├── data.xlsx
├── data.json
```

The program:
- Reads the Excel file and range defined in the JSON
- Groups rows using a threshold
- Outputs a `.txt` file with group IDs you can paste into Excel

---

## ⚙️ Efficient Grouping with Threshold Dictionary

Internally, ExcelGrouper uses a structure called `ThresholdGroupedDictionary` to assign group IDs.

- It compares incoming rows to already grouped rows.
- If a match is found **within the threshold** across all specified headers, it assigns the same group ID.
- If not, it creates a new group.

### 🔍 Why It’s Fast

- Uses **hash-based mapping** to manage groups.
- Scales efficiently regardless of how many columns you select (headers).
- Quick lookup avoids expensive full comparisons for each new row.

This structure ensures performance stays smooth even with over 100k of rows.

---



## 🚀 Usage

Create a `.json` file with the same name as your Excel file with the following template.

```json
{
  "worksheetName": "Sheet1",
  "cellsRange": "A1:C9",
  "headers": ["RequiredHeader1", "RequiredHeader2", "RequiredHeader3"],
  "threshold": 2
}
```

### Field Descriptions:
- `"worksheetName"` — Name of the worksheet to use
- `"cellsRange"` — The range to analyze (including the header row)
- `"headers"` — List of columns to base grouping on (must match header row text and order)
- `"threshold"` — Whole number (positive or negative doesn't matter, the value is checked both for positive and negative); defines how far apart values can be to still be grouped together

---

⚠️ In **both methods below**, the `.xlsx` and `.json` files must:
- Be in the **same folder**
- Have the **same base name**

### Option 1: Command Line
```bash
ExcelGrouper path\to\your\config.json
```

### Option 2: Drag and Drop
Drag the `.json` file onto `ExcelGrouper.exe`.

> ✅ The `.exe` file can be **anywhere** — as long as the dragged or specified `.json` file is valid and points to a matching `.xlsx` file in the **same folder**.

---

## 📤 Output

A `.txt` file will be created in the same folder, named:

```
<ExcelFileName>_<SheetName>.txt
```

- It will contain one group ID per row (excluding the header)
- You can paste this as a new column in Excel
- Group IDs align 1-to-1 with the rows in your range

Example output (`data_Sheet1.txt`):
```
1
1
2
3
3
```

---

## 📌 Notes

- The Excel file **must be closed** when running the tool
- Grouping is based on absolute difference in values vs the threshold
- Uses [ClosedXML](https://github.com/ClosedXML/ClosedXML) as a third-party library under the MIT license (already included)

---

## ✅ Example Use

### Given:
- Excel file: `results.xlsx`
- Config file: `results.json`
```json
{
  "worksheetName": "Sheet1",
  "cellsRange": "A1:D10",
  "headers": ["Speed", "Power", "Accuracy"],
  "threshold": 3
}
```

### Then:
- Run the program or drag the `.json` file to the `.exe`
- Output: `results_Sheet1.txt` with group IDs ready to paste back into Excel

---

## 🗂 File Structure Example

Your folder can look like this:

```
📁 /your-folder/
├── results.xlsx
├── results.json
└── results_Sheet1.txt  <-- Output
```

> The location of `ExcelGrouper.exe` does **not** matter — you can drag `results.json` from anywhere.
