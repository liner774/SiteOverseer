$(document).ready(function () {
    $('.showTableButton').click(function () {
        $('#tableModal').modal('show');
    });
});


function addPackageItems() {
    const tBody = $('#packageItemsListId');
    const modal = $('#packageItemsModal');

    const columnStyles = {
        padding: '0px',
        height: '30px',
        width: '100%',
        border: 'none',
        color: '#31849B',
        textAlign: 'center'
    };

    const leftColumnStyles = {
        ...columnStyles,
        textAlign: 'left',
    };

    const newRow = $('<tr>').css({ 'font-size': '14px', 'text-align': 'center' });

    //select ItemId

    var selectItemId = $('<select>').css(leftColumnStyles);

    $.ajax({
        url: "/StockPackage/GetStocks",
        success: function (stocks) {
            var fragment = document.createDocumentFragment();
            stocks.forEach(stock => {
                $("<option>").val(stock.itemId).text(stock.itemId).appendTo(fragment);
            });
            selectItemId.append(fragment);
        },
        error: function () {
            alert('Error fetching stocks.');
        }
    });

    selectItemId.on('change', function () {
        $.ajax({
            url: "/StockPackage/GetStockById",
            data: { itemId: this.value },
            success: function (stock) {
                inputItemDesc.val(stock.itemDesc);
            },
            error: function () {
                alert('Error fetching Stocks.');
            }
        });

        $.ajax({
            url: "/StockPackage/GetStockUOMs",
            data: { itemId: this.value },
            success: function (uoms) {
                selectUOM.find("option").remove();
                uoms.forEach(uom => {
                    $("<option>").val(uom.uomCde).text(uom.uomCde).appendTo(selectUOM);
                });
            },
            error: function () {
                alert('Error fetching UOMs.');
            }
        });
    });

    newRow.append($('<td>').css('padding', '0px').append(selectItemId));

    //input itemDesc
    var inputItemDesc = $('<input>').css(leftColumnStyles);
    newRow.append($('<td>').css('padding', '0px').append(inputItemDesc));

    //select UOM

    var selectUOM = $('<select>').css(leftColumnStyles);

    $.ajax({
        url: "/GoodReceive/GetStockUOMs",
        data: { itemId: selectItemId.val() },
        success: function (uoms) {
            uoms.forEach(uom => {
                $("<option>").val(uom.uomCde).text(uom.uomCde).appendTo(selectUOM);
            });
        },
        error: function () {
            alert('Error fetching UOMs.');
        }
    });

    newRow.append($('<td>').css('padding', '0px').append(selectUOM));

    //input Qty

    var inputQty = $('<input>').attr('type', 'number').css(columnStyles).val(1);
    newRow.append($('<td>').css('padding', '0px').append(inputQty));

    newRow.on('keypress', function (event) {
        if (event.keyCode === 13) {
            addPackageItems();
        }
    });


    newRow.on('keydown', function (event) {
        if (event.keyCode === 46) {
            newRow.remove();
        }
    });
    tBody.append(newRow);
}
function callWbsCodesController() {
    $.ajax({
        url: "/Wbss/GetWbsDetails",
        type: "GET",
        success: function (data) {
            $('#tableModal').show();
        },
        error: function (data) {
            alert('error');
        }
    });
}
function editWBSCodes(wbsId) {

    console.log("I am here");

    const tBody = $('#wbsCodesBodyId');
    const modal = $('#tableModal');

    var inputData = { id: wbsId };

    $.ajax({
        type: 'GET',
        url: '/Wbss/GetWbsDetails',
        data: inputData,
        success: function (list) {

            const columnStyles = {
                padding: '0px',
                height: '30px',
                width: '100%',
                border: 'none',
                color: 'black',
                textAlign: 'center',
               
            };

            const leftColumnStyles = {
                ...columnStyles,
                textAlign: 'left',
            };
            list.forEach(function (item) {

                const newRow = $('<tr>').css({ 'font-size': '14px', 'text-align': 'center' });
              
                //newRow.on('keypress', function (event) {
                //    if (event.keyCode === 13) {
                //        addPackageItems();
                //    }
                //});

                //newRow.on('keydown', function (event) {
                //    if (event.keyCode === 46) {
                //        newRow.remove();
                //    }
                //});

                tBody.append(newRow);

            });
            modal.show();

            //input wbsCode
            var inputWBSCode = $('<input>').css(leftColumnStyles).val(item.wbsdCde);
            newRow.append($('<td>').css('padding', '0px').append(inputWBSCode));



            // Add the Actions column with a trash bin icon
            var deleteButton = $('<button>')
                .addClass('btn btn-danger btn-sm')
                .css({
                    'width': '30px',
                    'height': 'auto',

                })
                .html('<span aria-hidden="true">&times;</span>')
                .on('click', clearPackageItems ());

            newRow.append($('<td>').css('padding', '0px').append(deleteButton));

        },
        error: function () {
            alert('Error occured.');
        }
    });

    



}
function clearPackageItems() {
    $('#wbsCodesBodyId').empty();
}
function closePackageItemModal() {
    $('#tableModal').modal('hide');
    $('.modal-backdrop').hide();
}
