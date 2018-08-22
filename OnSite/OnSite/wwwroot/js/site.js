//================================================================
// Page Load - Handling for URLs
//================================================================
$(document).ready(function () {
    var pathname = window.location.pathname;
    var urlSplit = pathname.split('/');

    // tab selection for manage visit page
    if (urlSplit[1] == "VisitManager") {
        if (urlSplit[2] == "Unapproved") {
            openVisitTab("unapproved-visits");
        } else if (urlSplit[2] == "Approved") {
            openVisitTab("approved-visits");
        }
    }
});

//================================================================
// Item selected from list of sites
//================================================================
$("#org-site-table tr").click(function () {

    // determine which item was selected
    var item_index = $(this).index() + 1;

    // get organisation associated with selected item
    var orgIDDisplay_ID = '#OrgIDDisplay_' + item_index;
    var selectedOrgID = $(orgIDDisplay_ID).val();

    // get organisation associated with selected item
    var siteIDDisplay_ID = '#SiteIDDisplay_' + item_index;
    var selectedSiteID = $(siteIDDisplay_ID).val();

    // show the item as selected and deselect other items
    $(this).addClass('selected').siblings().removeClass('selected');

    // select so the model can be updated as needed
    $("#site-id-select select").val(selectedSiteID);

    // show the areas associated with the selected org / site
    $.get("/SignIn?handler=Areas&OrgId=" + selectedOrgID +
        "&SiteId=" + selectedSiteID, function (response) {
     
            var areaTableBody = $("#area-table-body");

            areaTableBody.empty();

            var obj = JSON.parse(response);
            jQuery(obj).each(function (i, item) {

                $(areaTableBody).append('<tr>'+
                    '<td>'+item.Floor+'</td>'+
                    '<td>'+item.Description+'</td>'+
                    '<td>'+item.Classification+'</td>'+
                    '</tr>');

                $("#area-table tr").click(function () {

                    // show the row as selected
                    $(this).addClass('selected').siblings().removeClass('selected');

                });
            });
    });

});

//================================================================
// Visitor entry text updated
//================================================================
function visitorIdentificationUpdated() {

    // get the value entered by the user in FirstName field
    var firstNameSearch = $('#firstNameInput').val();
    var lastNameSearch = $('#lastNameInput').val();
    var identificationNumberSearch = $('#identificationNumberInput').val();

    // get visitor data to be displayed in list
    $.get("/SignIn?handler=Visitors&FirstName=" + firstNameSearch +
        "&LastName=" + lastNameSearch +
        "&IdentificationNumber=" + identificationNumberSearch, function (response) {
            displayExistingVisitorList(response) 
    });
}

//================================================================
// Populate list of existing visitors when search data entered
//================================================================
function displayExistingVisitorList(visitorInfo) {

    // select the html element and clear it
    var visitorTableBody = $("#visitor-table-body");
    visitorTableBody.empty();

    var obj = JSON.parse(visitorInfo);
    var count = 0;
    jQuery(obj).each(function (i, item) {

        count++;

        // set organisation name based on id
        setVisitorOrgName(item.OrganisationId, count);

        $(visitorTableBody).append('<tr data-visitorId = ' + item.VisitorId + '>' +
            '<td id="FirstNameDisplay_' + count + '">' + item.FirstName + '</td>' +
            '<td id="LastNameDisplay_' + count + '">' + item.LastName + '</td>' +
            '<td id="OrganisationDisplay_' + count + '"></td>' +
            '<td id="IdentificationNumberDisplay_' + count + '">' + item.IdentificationNumber + '</td>' +
            '</tr>');

        // bind function to row selection
        $('#visitor-table tr').click(function () {

            //var selectedRow = $(this);
            existingVisitorSelected($(this)); 

        });
    });
}

//================================================================
// Get the name of an organisation based on its id
//================================================================
function setVisitorOrgName(orgId, rowNum) {

    $.get("/SignIn?handler=OrganisationName&OrganisationId=" + orgId, function (result) {
        organisationName = result;
        $('#OrganisationDisplay_' + rowNum).html(organisationName);
    });
}

//================================================================
// Existing visitor record selected from list
//================================================================
function existingVisitorSelected(selectedRow) {

    // determine which row was selected
    var row_index = selectedRow.index() + 1;

    // get the id of the selected visitor
    var selectedVisitor = selectedRow.attr("data-visitorId");

    // show the row as selected
    selectedRow.addClass('selected').siblings().removeClass('selected');

    // store the selected visitor id to use when creating visit record
    $("#visitor-id-select select").val(selectedVisitor);

    // populate the fields when an item from the list is selected
    // get selected first name
    var firstNameDisplay_ID = '#FirstNameDisplay_' + row_index;
    $('#firstNameInput').val($(firstNameDisplay_ID).html());

    // get selected last name
    var lastNameDisplay_ID = '#LastNameDisplay_' + row_index;
    $('#lastNameInput').val($(lastNameDisplay_ID).html());

    // get the name of the organisation based on its id
    var organisationDisplay_ID = '#OrganisationDisplay_' + row_index;

    // organisation text input field
    var textInput = $('.combobox-input')[0];

    // value selected from visitor list
    var selectedValue = $(organisationDisplay_ID).html()

    // show correct item as selected
    for (var i = 0; i < document.getElementsByClassName('combobox-option').length; i++) {

        // get next item from list of options
        var elem = $('.combobox-option')[i];
        if (selectedValue == $(elem).html()) {
            $(textInput).val($(elem).text());
            elem.style.backgroundColor = '#555555';
            elem.style.color = '#fff';
        } else {
            elem.style.backgroundColor = '';
            elem.style.color = '#000';
        }
    }

    // get selected identification number
    var identificationNumberDisplay_ID = '#IdentificationNumberDisplay_' + row_index;
    $('#identificationNumberInput').val($(identificationNumberDisplay_ID).html());
}

//================================================================
// Submit button pressed
//================================================================
function signInSubmitted() {

    // verify that an organisation name has been correctly entered/selected
    orgValid = /^[A-Za-z0-9 ,.'-]+$/.test($("#visitor-org-select").val());
    if (!orgValid) {
        $('#sign-in-org-error').show();
        return false;
    } else {
        return true;
    }

    // [TODO] Add validation to ensure a site is selected
};

//================================================================
// Tabbed content on the manage visits page
//================================================================
function openVisitTab(tabId) {

    // hide all tab content
    document.getElementById("unapproved-visits").style.display = "none";
    document.getElementById("approved-visits").style.display = "none";

    // deselect all tabs
    var tabButtons = document.getElementsByClassName("tab-button");
    for (var i = 0; i < tabButtons.length; i++) {
        tabButtons[i].className = tabButtons[i].className.replace(" active", "");
    }

    // display the requested tab content
    document.getElementById(tabId).style.display = "block";
    document.getElementById(tabId + "-tab").className += " active";

    // update the url to reflect the currently selected tab
    if (tabId == "unapproved-visits") {
        var tabURL = "Unapproved";
    } else {
        var tabURL = "Approved";
    }
    var newUrl = "/VisitManager/" + tabURL;
    window.history.pushState("data", "Title", newUrl);
}

//================================================================
// Show combo box dropdown menu
//================================================================
function showComboboxDropdown() {

    var optionDiv = document.getElementById('combobox-options');
    if (optionDiv.style.display == 'block') {
        optionDiv.style.display = 'none';
    } else {
        optionDiv.style.display = 'block';
    }
}

//================================================================
// Text entered in combo box input field
//================================================================
function inputUpdated() {

    for (var i = 0; i < document.getElementsByClassName('combobox-option').length; i++) {

        // get next item from list of options
        var elem = $('.combobox-option')[i];

        // get the text entered in input field
        var textInput = $('.combobox-input')[0];

        // check if the entered text matches an item in the list
        if ($(textInput).val().toLowerCase() == $(elem).html().toLowerCase()) {
            $(textInput).val($(elem).text());
            elem.style.backgroundColor = '#555555';
            elem.style.color = '#fff';
        } else {
            elem.style.backgroundColor = '';
            elem.style.color = '#000';
        }
    }
}

//================================================================
// Select item from combo box list
//================================================================
function selectComboItem(itemNum) {

    // text entry box
    var textInput = $('.combobox-input')[0];

    // item selected from drop down menu
    var selectedItem = $('.combobox-option')[itemNum];

    // get text value of the selected item
    $(textInput).val($(selectedItem).text());

    // remove drop down menu from display
    document.getElementById('combobox-options').style.display = 'none';

    // set the display of each item in the drop down list
    for (var i = 0; i < document.getElementsByClassName('combobox-option').length; i++) {
        var elem = document.getElementsByClassName('combobox-option')[i];
        // show selected item as selected
        if (elem == selectedItem) {
            elem.style.backgroundColor = '#555555';
            elem.style.color = '#fff';
            // show all other items as deselected
        } else {
            elem.style.backgroundColor = '';
            elem.style.color = '#000';
        }
    }
}

//================================================================
// Load unapproved visit data
//================================================================
function loadUnapprovedVisitData(visitId) {

    // display visit data and display
    $.get("/VisitManager?handler=VisitData&visitId=" + visitId, function (response) {
        var obj = JSON.parse(response);
        jQuery(obj).each(function (i, item) {
            $("#visit-vehicleid-entry").val(item.VehicleId);
            $("#visit-description-entry").val(item.Description);
        });
    });

    // get visitor data and display
    $.get("/VisitManager/Unapproved?handler=VisitorData&visitId=" + visitId, function (response) {
        var obj = JSON.parse(response);
        jQuery(obj).each(function (i, item) {

            // populate fields with visitor data
            $("#firstNameInput").val(item.FirstName);
            $("#lastNameInput").val(item.LastName);
            $("#identificationNumberInput").val(item.IdentificationNumber);

            // get the organisation name based on its id
            $.get("/SignIn?handler=OrganisationName&OrganisationId=" + item.OrganisationId, function (result) {
                organisationName = result;
                $('#visitor-org-select').val(organisationName);
                inputUpdated();
            });
        });
    });

}

//================================================================
// Staff Member selected from list
//================================================================
function staffMemberSelected(staffId) {

    // store the selected staff id
    $("#staff-id-select input").val(staffId);

    // show the row as selected
    $("#staff-table-row-" + staffId).addClass('selected').siblings().removeClass('selected');
}

//================================================================
// Staff Member search text updated
//================================================================
function searchStaff() {

    // get the value entered by the user in search field
    var searchTerm = $('#staff-select-search').val();

    // get staff member records matching search
    $.get("/StaffAccessManager?handler=StaffSearch&SearchString="
        + searchTerm, function (response) {

        // select the existing staff table and clear it
        var staffTableBody = $("#select-staff-table-body");
        staffTableBody.empty();

        // populate the table using the query results
        var obj = JSON.parse(response);
        jQuery(obj).each(function (i, item) {

            // add staff member record to the table
            $(staffTableBody).append(
                '<tr class="staff-table-row" id="staff-table-row-' +
                    item.StaffId + '" onclick="staffMemberSelected(' + item.StaffId + ')">' +
                    '<td>' + item.FirstName + '</td>' +
                    '<td>' + item.LastName + '</td>' +
                    '<td>' + item.Position + '</td>' +
                '</tr>');
        });

        // show staff member as selected
        staffMemberSelected($("#staff-id-select input").val());

    });
}
