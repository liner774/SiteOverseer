function changeWBSDetails() {
    var wbsCodeDropdown = document.getElementById("wbsCodeDropdown");
    var wbsDetailDropdown = document.getElementById("wbsDetailDropdown");
    var selectedWbsId = wbsCodeDropdown.value;

    // Clear existing options
    wbsDetailDropdown.innerHTML = '<option value="">- Select One -</option>';

    // Fetch WBS Details based on selected WBS Code
    fetch(`/FacilityProgresses/GetWbsDetails?wbsId=${selectedWbsId}`)
        .then(response => response.json())
        .then(data => {
            data.forEach(function (wbsDetail) {
                var option = document.createElement("option");
                option.value = wbsDetail.value;
                option.textContent = wbsDetail.text;
                wbsDetailDropdown.appendChild(option);
            });
        });
}



