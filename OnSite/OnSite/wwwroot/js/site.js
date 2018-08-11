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

    $.get("/SignIn?handler=Visitors&FirstName=" + firstNameSearch +
        "&LastName=" + lastNameSearch +
        "&IdentificationNumber=" + identificationNumberSearch, function (response) {

            var visitorTableBody = $("#visitor-table-body");
        visitorTableBody.empty();

            var obj = JSON.parse(response);
            var count = 0;
            jQuery(obj).each(function (i, item) {

                count++;

                var organisationName = null;
                $.get("/SignIn?handler=OrganisationName&OrganisationId=" + item.OrganisationId, function (response) {
                    organisationName = response;    
                

                $(visitorTableBody).append('<tr data-visitorId = ' + item.VisitorId+'>' +
                    '<td id="FirstNameDisplay_'+count+'">' + item.FirstName + '</td>' +
                    '<td id="LastNameDisplay_' + count + '">' + item.LastName + '</td>' +
                    '<td id="OrganisationDisplay_' + count + '">' + organisationName + '</td>' +
                    '<td id="IdentificationNumberDisplay_' + count + '">' + item.IdentificationNumber + '</td>' +
                        '</tr>');



                    // bind function to row selection
                    $('#visitor-table tr').click(function () {

                        // determine which row was selected
                        var row_index = $(this).index() + 1;

                        // get the id of the selected visitor
                        var selectedVisitor = $(this).attr("data-visitorId");

                        // show the row as selected
                        $(this).addClass('selected').siblings().removeClass('selected');

                        // populate the fields when an item from the list is selected
                        var firstNameDisplay_ID = '#FirstNameDisplay_' + row_index;
                        $('#firstNameInput').val($(firstNameDisplay_ID).html());

                        // get selected last name
                        var lastNameDisplay_ID = '#LastNameDisplay_' + row_index;
                        $('#lastNameInput').val($(lastNameDisplay_ID).html());

                        // get the name of the organisation based on its id
                        // var organisationDisplay_ID = '#OrganisationDisplay_' + row_index;
                        // $('#organisationInput').val("g");

                        var identificationNumberDisplay_ID = '#IdentificationNumberDisplay_' + row_index;
                        $('#identificationNumberInput').val($(identificationNumberDisplay_ID).html());

               /* lastNameInput
                visitorOrganisationInput
                identificationNumberInput*/



                });
            });

            

            });
        });
}


