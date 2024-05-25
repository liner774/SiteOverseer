﻿
function editWBSCodes(wbsId) {
    const $tBody = $('#wbsCodesBodyId');
    const $modal = $('#tableModal');

    const inputData = { id: wbsId };

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
                backgroundColor: 'white'
            };

            const leftColumnStyles = {
                ...columnStyles,
                textAlign: 'left',
            };

            //no duplicate
            $tBody.empty();
            list.forEach(function (item) {
                const $newRow = $('<tr>').css({ 'font-size': '14px', 'text-align': 'center' });

                const $inputWBSCode = $('<input>').css(leftColumnStyles).val(item.wbsdCde);
                $newRow.append($('<td>').css('padding', '0px').append($inputWBSCode));

                const $deleteButton = $('<button>')
                    .addClass('btn btn-danger btn-sm')
                    .css({
                        'width': '30px',
                        'height': 'auto',
                    })
                    .html('<i class="fa fa-trash"></i>')
                    .on('click', clearPackageItems ());
                $newRow.append($('<td>').css('padding', '0px').append($deleteButton));

                $tBody.append($newRow);
            });

            $modal.modal('show');
        },
        error: function () {
            alert('Error occurred.');
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

function addWBSCodes() {
    const $tBody = $('#wbsCodesBodyId');

    const columnStyles = {
        padding: '0px',
        height: '30px',
        width: '100%',
        border: 'none',
        color: 'black',
        textAlign: 'center',
        backgroundColor: 'white'
    };

    const leftColumnStyles = {
        ...columnStyles,
        textAlign: 'left',
    };

    // Create new row
    const $newRow = $('<tr>').css({ 'font-size': '14px', 'text-align': 'center' });

    // Input WBSCode
    const $inputWBSCode = $('<input>').css(leftColumnStyles);
    $newRow.append($('<td>').css('padding', '0px').append($inputWBSCode));

    // Trash Button 
    const $deleteButton = $('<button>')
        .addClass('btn btn-danger btn-sm')
        .css({
            'width': '30px',
            'height': 'auto',
        })
        .html('<i class="fa fa-trash"></i>')
        .on('click', function () {
            $(this).closest('tr').remove();
        });
    $newRow.append($('<td>').css('padding', '0px').append($deleteButton));

    $tBody.append($newRow);


    $inputWBSCode.focus();
}

function saveWBSCodes() {
    const modal = $('#tableModal');
    const shouldProceed = window.confirm("Are you sure you want to save the records?");
    
    if (shouldProceed) {
        const wbsCodes = [];
        const table = document.getElementById('packageItemsTable');

        for (let i = 1; i < table.rows.length; i++) {
            const row = table.rows[i];
            const wbsCode = $(row.cells[0]).find('input').val().trim();

            
            if (wbsCode) {
                wbsCodes.push(wbsCode);
            }
        }

        console.log(wbsCodes);

        if (wbsCodes.length === 0) {
            alert('No data to save. Please add WBSCodes.');
            return;
        }

        var inputData = { wbsDetails : wbsCodes};

        $.ajax({
            type: 'POST',
            url: '/Wbss/SaveWbsDetails', 
            data: inputData,
            success: function (response) {
                if (response.success) {
                    alert('WBSCodes saved successfully!');
                    modal.modal('hide');
                    location.reload(); 
                } else {
                    alert('Error saving WBSCodes: ' + response.message);
                }
            },
            error: function () {
                alert('Error occurred while saving WBSCodes.');
            }
        });
    }
}



