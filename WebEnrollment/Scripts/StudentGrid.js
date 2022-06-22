$(function () {
    $("#studentGrid").jqGrid({
        url: "/Student/GetStudents",
        datatype: 'json',
        mtype: 'Get',
        colNames: ["StudentID", "StudentNumber", "First Name", "Last Name", "Email", "Mobile", "Program", "Level"],
        colModel: [
            {
                key: true, name: 'StudentID', index: 'StudentID', sortable: false, editable: true, hidden: true
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
            //{
            //    key: false, name: 'Birthday', index: 'Birthday', editable: true, formatter: 'date', formatoptions: { newformat: 'd/m/Y' }
            //},
            {
                key: false, name: 'Program', index: 'Program', editable: true
            },
            {
                key: false, name: 'Level', index: 'Level', editable: true
            }
        ],
        //colNames: ['ID', 'Student Name', 'Father Name', 'Gender', 'Class', 'Admission Date'],
        //colModel: [
        //    { key: true, hidden: true, name: 'ID', index: 'ID', editable: true },
        //    { key: false, name: 'Name', index: 'Name', editable: true },
        //    { key: false, name: 'FatherName', index: 'FatherName', editable: true },
        //    { key: false, name: 'Gender', index: 'Gender', editable: true, edittype: 'select', editoptions: { value: { 'M': 'Male', 'F': 'Female', 'N': 'None' } } },
        //    { key: false, name: 'ClassName', index: 'ClassName', editable: true, edittype: 'select', editoptions: { value: { '1': '1st Class', '2': '2nd Class', '3': '3rd Class', '4': '4th Class', '5': '5th Class' } } },
        //    { key: false, name: 'DateOfAdmission', index: 'DateOfAdmission', editable: true, formatter: 'date', formatoptions: { newformat: 'd/m/Y' } }],
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
            url: '/Student/Edit',
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
        },
        {
            zIndex: 100,
            caption: "Search Students",
            sopt: ['cn']
        });
});