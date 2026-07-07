---
component: Table
tags: [dui-table, dui-table-body, dui-table-caption, dui-table-cell, dui-table-footer, dui-table-head, dui-table-header, dui-table-row]
generated: true
---

# Table

A responsive data table, rendered as a `<table>` inside a scrollable container. Compose it with the header, body, footer, row, head, cell, and caption subcomponents.

## Tags

| Tag | Description |
|-----|-------------|
| `<dui-table>` | A responsive data table, rendered as a `<table>` inside a scrollable container. Compose it with the header, body, footer, row, head, cell, and caption subcomponents. |
| `<dui-table-body>` | The body of a table, rendered as a `<tbody>`; contains the data rows. |
| `<dui-table-caption>` | A caption for a table, rendered as a `<caption>`; describes the table's contents. |
| `<dui-table-cell>` | A data cell within a table row, rendered as a `<td>`. |
| `<dui-table-footer>` | The footer of a table, rendered as a `<tfoot>`; typically holds summary rows. |
| `<dui-table-head>` | A header cell within a table header row, rendered as a `<th>`. |
| `<dui-table-header>` | The header section of a table, rendered as a `<thead>`; contains the header row. |
| `<dui-table-row>` | A row within a table, rendered as a `<tr>`. |

## Examples

*From `Pages/Table/_Intro.cshtml`*

```razor
<dui-table>
        <dui-table-caption>Total Active Value includes Confirmed and Pending bookings only.</dui-table-caption>
    <dui-table-header>
        <dui-table-row>
            <dui-table-head class="w-[100px]">Booking #</dui-table-head>
            <dui-table-head>Status</dui-table-head>
            <dui-table-head>Destination</dui-table-head>
            <dui-table-head class="text-right">Amount</dui-table-head>
        </dui-table-row>
    </dui-table-header>
    <dui-table-body>
        @foreach (var booking in StaticData.Bookings)
        {
            <dui-table-row>
                <dui-table-cell class="font-medium">@booking.Id</dui-table-cell>
                <dui-table-cell>
                    <dui-badge variant="@GetBadgeVariant(booking.Status)">
                        @booking.Status.ToString()
                    </dui-badge>
                </dui-table-cell>
                <dui-table-cell>@booking.Destination</dui-table-cell>
                <dui-table-cell class="text-right">
                    @booking.Amount.ToString("N")
                </dui-table-cell>
            </dui-table-row>    
        }
    </dui-table-body>
    <dui-table-footer>
        <dui-table-row>
            <dui-table-cell colspan="3">
                Total Active Value
            </dui-table-cell>
            <dui-table-cell class="text-right">
                @{
                    var activeValue = StaticData.Bookings
                        .Where(b => b.Status != BookingStatus.Cancelled)
                        .Select(b => b.Amount)
                        .Sum();
                }
                @activeValue.ToString("N")
            </dui-table-cell>
        </dui-table-row>
    </dui-table-footer>
</dui-table>

@functions
{
    BadgeVariant GetBadgeVariant(BookingStatus status) => status switch
    {
        BookingStatus.Cancelled => BadgeVariant.Destructive,
        BookingStatus.Pending => BadgeVariant.Secondary,
        _ => BadgeVariant.Default
    };
}
```

*From `Pages/Table/_Select.cshtml`*

```razor
    var bookingStatusList = Html.GetEnumSelectList<BookingStatus>();
}
<dui-table>
    <dui-table-caption>Total Active Value includes Confirmed and Pending bookings only.</dui-table-caption>
    <dui-table-header>
        <dui-table-row>
            <dui-table-head class="w-[100px]">Booking #</dui-table-head>
            <dui-table-head>Status</dui-table-head>
            <dui-table-head>Destination</dui-table-head>
            <dui-table-head class="text-right">Amount</dui-table-head>
        </dui-table-row>
    </dui-table-header>
    <dui-table-body>
        @foreach (var booking in StaticData.Bookings)
        {
            <dui-table-row>
                <dui-table-cell class="font-medium">@booking.Id</dui-table-cell>
                <dui-table-cell>
                    <dui-select size="SelectSize.Small">
                        @foreach (var selectListItem in bookingStatusList)
                        {
                            var selected = BookingStatus.TryParse(selectListItem.Value, out BookingStatus status) && booking.Status == status;
                            
                            <option value="@selectListItem.Value" selected="@(selected)">@selectListItem.Text</option>
                        }
                    </dui-select>
                </dui-table-cell>
                <dui-table-cell>@booking.Destination</dui-table-cell>
                <dui-table-cell class="text-right">
                    @booking.Amount.ToString("N")
                </dui-table-cell>
            </dui-table-row>    
        }
    </dui-table-body>
    <dui-table-footer>
        <dui-table-row>
            <dui-table-cell colspan="3">
                Total Active Value
            </dui-table-cell>
            <dui-table-cell class="text-right">
                @{
                    var activeValue = StaticData.Bookings
                        .Where(b => b.Status != BookingStatus.Cancelled)
                        .Select(b => b.Amount)
                        .Sum();
                }
                @activeValue.ToString("N")
            </dui-table-cell>
        </dui-table-row>
    </dui-table-footer>
</dui-table>
```
