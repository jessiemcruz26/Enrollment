$(function () {
    $("#courseGrid").jqGrid({
        url: "/Course/GetCourses",
        datatype: 'json',
        mtype: 'Get',
        colNames: ["CourseID", "Course Name", "Course Description"],
        colModel: [
            {
                key: true, name: 'CourseID', index: 'CourseID', sortable: false, editable: true
            },
            {
                name: 'CourseName', index: 'CourseName', sortable: false, editable: true
            },
            {
                name: 'CourseDescription', index: 'CourseDescription', editable: true
            },
          
            //{
            //    name: 'Birthday', index: 'Birthday', editable: true, formatter: 'date', formatoptions: { newformat: 'd/m/Y' }
            //},
            //{
            //    name: 'Program', index: 'Program', editable: true
            //},
            //{
            //    name: 'Level', index: 'Level', editable: true
            //}
        ],
        //colNames: ['ID', 'Student Name', 'Father Name', 'Gender', 'Class', 'Admission Date'],
        //colModel: [
        //    { key: true, hidden: true, name: 'ID', index: 'ID', editable: true },
        //    { key: false, name: 'Name', index: 'Name', editable: true },
        //    { key: false, name: 'FatherName', index: 'FatherName', editable: true },
        //    { key: false, name: 'Gender', index: 'Gender', editable: true, edittype: 'select', editoptions: { value: { 'M': 'Male', 'F': 'Female', 'N': 'None' } } },
        //    { key: false, name: 'ClassName', index: 'ClassName', editable: true, edittype: 'select', editoptions: { value: { '1': '1st Class', '2': '2nd Class', '3': '3rd Class', '4': '4th Class', '5': '5th Class' } } },
        //    { key: false, name: 'DateOfAdmission', index: 'DateOfAdmission', editable: true, formatter: 'date', formatoptions: { newformat: 'd/m/Y' } }],
        pager: jQuery('#courseControls'),
        rowNum: 10,
        rowList: [10, 20, 30, 40, 50],
        height: '100%',
        viewrecords: true,
        caption: 'Course Records',
        emptyrecords: 'No Course Records are Available to Display',
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
    }).navGrid('#courseControls', {
        edit: true, add: true, del: true, search: true,
        searchtext: "Search Course", refresh: true
    },
        {
            zIndex: 100,
            url: '/Course/EditGrid',
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
            url: "/Course/Create",
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
            url: "/Course/Delete",
            closeOnEscape: true,
            closeAfterDelete: true,
            recreateForm: true,
            msg: "Are you sure you want to delete Course... ? ",
            afterComplete: function (response) {
                if (response.responseText) {
                    alert(response.responseText);
                }
            }
        },
        {
            zIndex: 100,
            caption: "Search Courses",
            sopt: ['cn']
        });
});