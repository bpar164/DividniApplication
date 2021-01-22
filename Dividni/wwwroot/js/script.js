$(document).ready(function () {
    $('.sidenav').sidenav();
    $(".dropdown-trigger").dropdown();
    $('.tooltipped').tooltip({
        enterDelay: 500
    });
    $(".dropdown-trigger").dropdown({
        constrainWidth: false,
        container: document.getElementById('main'),
    });
    $(".dropdown-trigger-nav").dropdown({
        constrainWidth: false,
        container: document.getElementById('main'),
        hover: true,
        coverTrigger: false
    });
    $('.modal').modal();
    $('.modal-not-dismissible').modal({ dismissible: false });
    $('#modal').modal('open');
    $('select').formSelect();
    //Ace Code Editor
    let editor = ace.edit("editor");
    editor.setTheme("ace/theme/monokai");
    editor.session.setMode("ace/mode/csharp");
});

