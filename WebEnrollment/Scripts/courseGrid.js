$(function () {
    $("#courseGrid").jqGrid({
        url: "/Course/GetCourses",
        datatype: 'json',
        mtype: 'Get',
        colNames: ["CourseID", "Course Name", "Course Description"],
        colModel: [
            {
                key: true, name: 'CourseID', index: 'CourseID', sortable: false, editable: true, hidden: true
            },
            {
                name: 'CourseName', index: 'CourseName', sortable: false, editable: true
            },
            {
                name: 'CourseDescription', index: 'CourseDescription', editable: true
            },
        ],
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