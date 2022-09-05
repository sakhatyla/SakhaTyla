$(function () {
    $('.translate-letters button').click(function () {
        var newText = $(this).text();
        var query = $('#query');
        var queryElement = query[0];

        var start = queryElement.selectionStart;
        var end = queryElement.selectionEnd;

        var before = queryElement.value.slice(0, start);
        var after = queryElement.value.slice(end);

        var text = before + newText + after;
        queryElement.value = text;

        $('#query').keydown();
    });
});