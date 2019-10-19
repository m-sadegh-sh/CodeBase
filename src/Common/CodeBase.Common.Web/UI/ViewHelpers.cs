namespace CodeBase.Common.Web.UI {
    using System;
    using System.Web.Mvc;

    public static class ViewHelpers {
        public static bool IsRowStart(this HtmlHelper helper,
                                      int index,
                                      int itemsPerRow) {
            if (itemsPerRow == 1)
                return true;

            var currentPosition = index + 1;

            if (currentPosition == 1)
                return true;

            return currentPosition == itemsPerRow + 1;
        }

        public static bool IsRowEnd(this HtmlHelper helper,
                                    int index,
                                    int itemsPerRow) {
            if (itemsPerRow == 1)
                return true;

            var currentPosition = index + 1;

            return currentPosition == itemsPerRow;
        }

        public static bool IsLastRow(this HtmlHelper helper,
                                     int index,
                                     int totalItems,
                                     int itemsPerRow) {
            if (itemsPerRow == totalItems)
                return true;

            var totalRows = Math.Round(totalItems/(decimal) itemsPerRow,
                                       MidpointRounding.ToEven);

            var currentPosition = index + 1;
            var currentRow = Math.Round(currentPosition/(decimal) itemsPerRow,
                                        MidpointRounding.ToEven);

            return currentRow == totalRows;
        }

        public static bool IsMiddleColumn(this HtmlHelper helper,
                                          int index,
                                          int itemsPerRow) {
            if (itemsPerRow == 1)
                return true;

            var middleColumn = Math.Round(itemsPerRow/2M,
                                          MidpointRounding.ToEven);

            var currentPosition = index + 1;
            var currentColumn = currentPosition%itemsPerRow;

            return currentColumn == middleColumn;
        }

        public static bool IsLastItem(this HtmlHelper helper,
                                      int index,
                                      int totalItems) {
            var currentPosition = index + 1;

            return currentPosition == totalItems;
        }
    }
}