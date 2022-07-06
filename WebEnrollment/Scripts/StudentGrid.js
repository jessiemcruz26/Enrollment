$(function () {
    $("#studentGrid").jqGrid({
        url: "/Student/GetStudents",
        datatype: 'json',
        mtype: 'Get',
        colNames: ["StudentID", "StudentNumber", "First Name", "Last Name", "Email", "Mobile", "Program", "Level"],
        colModel: [
            {
                key: true, name: 'StudentID', index: 'StudentID', editable: true, hidden: true
            },
            {
                key: false, name: 'StudentNumber', index: 'StudentNumber', editable: true
            },
            {
                key: false, name: 'FirstName', index: 'FirstName', sortable: false, editable: true
            },
            {
                key: false, name: 'LastName', index: 'LastName', editable: true
            },
            {
                key: false, name: 'Email', index: 'Email', editable: true
            },
            {
                key: false, name: 'Mobile', index: 'Mobile', editable: true
            },
            {
                key: false, name: 'Program', index: 'Program', class: 'a', editable: true, cellEdit: false, edittype: 'select',
                formatter: function (cellvalue, options, rowObj) {
                    return cellvalue;
                }, editoptions: { value: getAllPrograms() }
            },
            {
                key: false, name: 'Level', index: 'Level', editable: true
            }
        ],
        pager: jQuery('#studentControls'),
        rowNum: 10,
        rowList: [10, 20, 30, 40, 50],
        height: '100%',
        viewrecords: true,
        caption: 'Students Records',
        emptyrecords: 'No Students Records are Available to Display',
        jsonReader: {
            root: "rows",
            page: "page",
            total: "total",
            records: "records",
            repeatitems: false,
            Id: "0"
        },
        autowidth: true,
        multiselect: false,
        hiddengrid: true
    }).navGrid('#studentControls', {
        edit: true, add: true, del: true, search: true,
        searchtext: "Search Student", refresh: true
    },
        {
            zIndex: 100,
            url: '/Student/EditGrid',
            closeOnEscape: true,
            closeAfterEdit: true,
            recreateForm: true,
            afterComplete: function (response) {
                if (response.responseText) {
                    alert(response.responseText);
                }
            }
        },
        {
            zIndex: 100,
            url: "/Student/Create",
            closeOnEscape: true,
            closeAfterAdd: true,
            afterComplete: function (response) {
                if (response.responseText) {
                    alert(response.responseText);
                }
            }
        },
        {
            zIndex: 100,
            url: "/Student/Delete",
            closeOnEscape: true,
            closeAfterDelete: true,
            recreateForm: true,
            msg: "Are you sure you want to delete Student... ? ",
            afterComplete: function (response) {
                if (response.responseText) {
                    alert(response.responseText);
                }
            }
        }
       );
});

function getAllPrograms() {
    var _text = ["Electronics", "Business", "Aviation", "Accounting"];
    var _programs = {};
    for (let i = 0; i < _text.length; i++) {
        _programs[_text[i]] = _text[i];
    }

    return _programs;
}