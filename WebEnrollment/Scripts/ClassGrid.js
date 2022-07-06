//const { error } = require("jquery");

//var _courses1 = {};

////$.ajax({
////    type: "GET",
////    url: "/Course/GetCourses",
////    contentType: "application/json; charset=utf-8",
////    dataType: "json",
////    async: false,
////    success: function (data, a) {
////        debugger;
////    }
////});

$(function () {
    $("#classGrid").jqGrid({
        url: "/Class/GetClasses",
        datatype: 'json',
        mtype: 'Get',
        colNames: ["ClassID", "Code", "Course", "Instructor", "Class Time", "Class Date", "Room Number"],
        colModel: [
            {
                key: true, name: 'ClassID', index: 'ClassID', sortable: false, editable: true, hidden: true
            },
            {
                name: 'ClassCode', index: 'ClassCode', editable: true
            },
            {
                key: false, name: 'CourseID', index: 'CourseID', editable: true, cellEdit: false, edittype: 'select',
                formatter: function (cellvalue, options, rowObj) {
                    return cellvalue;
                }, editoptions: { value: getAllCourses() }
            },
            {
                key: false, name: 'InstructorID', index: 'InstructorID', editable: true, cellEdit: false, edittype: 'select',
                formatter: function (cellvalue, options, rowObj) {
                    return cellvalue;
                }, editoptions: { value: getAllInstructors() }
            },
            {
                key: false, name: 'ClassTime', index: 'ClassTime', editable: true, cellEdit: false, edittype: 'select',
                formatter: function (cellvalue, options, rowObj) {
                    return cellvalue;
                }, editoptions: { value: getTime1() }
            },
            {
                key: false, name: 'ClassDate', index: 'ClassDate', editable: true, edittype: 'select',
                editoptions: { value: { 'MWF': 'MWF', 'TThS': 'TThS' } }
            },
            {
                name: 'RoomNumber', index: 'RoomNumber', editable: true
            },
        ],
        //colNames: ['ID', 'Student Name', 'Father Name', 'Gender', 'Class', 'Admission Date'],
        //colModel: [
        //    { key: true, hidden: true, name: 'ID', index: 'ID', editable: true },
        //    { key: false, name: 'Name', index: 'Name', editable: true },
        //    { key: false, name: 'FatherName', index: 'FatherName', editable: true },
        //    { key: false, name: 'Gender', index: 'Gender', editable: true, edittype: 'select', editoptions: { value: { 'M': 'Male', 'F': 'Female', 'N': 'None' } } },
        //    { key: false, name: 'ClassName', index: 'ClassName', editable: true, edittype: 'select', editoptions: { value: { '1': '1st Class', '2': '2nd Class', '3': '3rd Class', '4': '4th Class', '5': '5th Class' } } },
        //    { key: false, name: 'DateOfAdmission', index: 'DateOfAdmission', editable: true, formatter: 'date', formatoptions: { newformat: 'd/m/Y' } }],
        pager: jQuery('#classControls'),
        rowNum: 10,
        rowList: [10, 20, 30, 40, 50],
        height: '100%',
        viewrecords: true,
        caption: 'Class Records',
        emptyrecords: 'No Class Records are Available to Display',
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
    }).navGrid('#classControls', {
        edit: true, add: true, del: true, search: true,
        searchtext: "Search Course", refresh: true
    },
        {
            zIndex: 100,
            url: '/Class/EditGrid',
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
            url: "/Class/Create",
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
            url: "/Class/Delete",
            closeOnEscape: true,
            closeAfterDelete: true,
            recreateForm: true,
            msg: "Are you sure you want to delete Class... ? ",
            afterComplete: function (response) {
                if (response.responseText) {
                    alert(response.responseText);
                }
            }
        },
        {
            zIndex: 100,
            caption: "Search Classes",
            sopt: ['cn']
        });
});

function getTime1() {
    var _time = ["7:30am-9:00am", "9:00am-10:30am", "10:30am-12:00nn", "12:00nn-1:30pm", "1:30pm-3:00pm", "3:00pm-4:30pm"];
    var _sched = {};
    for (let i = 0; i < _time.length; i++) {
        _sched[_time[i]] = _time[i];
    }

    return _sched;
}

function getAllCourses() {
    var _courses = {};
    $.ajax({
        type: "GET",
        url: "/Course/GetCourses",
         contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (data) {
            if (data.success) {
                for (let i = 0; i < data.rows.length; i++) {
                    _courses[data.rows[i].CourseID] = data.rows[i].CourseName;
                }
            }
        }
    });

    return _courses;
}

function getAllInstructors() {
    var _instructors = {};
    $.ajax({
        type: "GET",
        url: "/Instructor/GetInstructors",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (data, status) {
            if (data.success) {
                for (let i = 0; i < data.rows.length; i++) {
                    _instructors[data.rows[i].InstructorID] = data.rows[i].FirstName + " " + data.rows[i].LastName;
                }
            }
        }
    });

    return _instructors;
}

