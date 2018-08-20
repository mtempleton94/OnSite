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

    $("#visitor-org-select").val($(organisationDisplay_ID).html());
    $("#visitor-org-combo").val($(organisationDisplay_ID).html());

    // get selected identification number
    var identificationNumberDisplay_ID = '#IdentificationNumberDisplay_' + row_index;
    $('#identificationNumberInput').val($(identificationNumberDisplay_ID).html());

}

//================================================================
// Submit button pressed
//================================================================
document.getElementById('sign-in-form').onsubmit = function () {

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
// Handling for custom combo box
//================================================================
$(function () {
    $.widget("combobox.combobox", {
        _create: function () {
            this.wrapper = $("<span>")
                .addClass("custom-combobox")
                .insertAfter(this.element);

            this.element.hide();
            this._autoComplete();
            this._dropDownButton();
        },

        _autoComplete: function () {
            var selected = this.element.children(":selected");
            var value = selected.val() ? selected.text() : "";

            // create input field to display value
            this.input = $("<input>")
                .appendTo(this.wrapper)
                .val(value)
                .attr("title", "")
                .attr("id", "visitor-org-combo")
                .addClass("ui-widget-content")
                .autocomplete({
                    delay: 0,
                    minLength: 0,
                    source: $.proxy(this, "_source")
                })
                .keyup(function (e) {
                    //set value of hidden field used by model
                    $("#visitor-org-select").val(this.value);
                    $('#sign-in-org-error').hide();
                })

            // select item in the original combo box
            this._on(this.input, {
                autocompleteselect: function (event, ui) {
                    ui.item.option.selected = true;

                    $("#visitor-org-select").val($("#visitor-organisation option:selected").text());
                    $('#sign-in-org-error').hide();
                    this._trigger("select", event, {
                        item: ui.item.option

                    });
                },
            });
        },

        _dropDownButton: function () {
            var input = this.input;
            var listOpen = false;
            $("<a>")
                .appendTo(this.wrapper)
                .button({
                    icons: {
                        primary: "icon.jpg"
                    },
                    text: false
                })
                .addClass("combobox-toggle")

                // check if list is open
                .on("mousedown", function () {
                    listOpen = input.autocomplete("widget").is(":visible");
                })

                // button clicked
                .on("click", function () {

                    // close the list if it is open
                    if (listOpen) {
                        return;
                    }

                    // empty search string - show all results
                    input.autocomplete("search", "");
                });
        },

        // autocomplete and display list based on entered value
        _source: function (request, response) {


            var matcher = new RegExp($.ui.autocomplete.escapeRegex(request.term), "i");
            response(this.element.children("option").map(function () {
                var text = $(this).text();
                if (this.value && (!request.term || matcher.test(text)))
                    return {
                        label: text,
                        value: text,
                        option: this
                    };
            }));
        },

        _destroy: function () {
            this.wrapper.remove();
            this.element.show();
        }
    });

    $(".combobox").combobox();
    $("#toggle").on("click", function () {
        $(".combobox").toggle();
    });
});

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



