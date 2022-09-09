$(function () {
    $('#query').autocomplete({
        source: function (request, response) {
            $.ajax({
                url: serverOptions.apiUrl + '/api/public/SuggestArticles',
                type: 'POST',
                data: JSON.stringify({ query: request.term }),
                contentType: 'application/json',
                dataType: 'json',
                success: function (data) {
                    response($.map(data, function (item) {
                        return {
                            label: item.title,
                            value: item.title
                        };
                    }));
                }
            });
        },
        minLength: $('#query').data('min-length'),
        select: function (event, ui) {
            $('#query').val(ui.item.value);
            $('#translateForm').submit();
        }
    }).bind('input', function () { $(this).keydown(); });

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

    $(document).on('click', '.article-more a', function () {
        $(this).parent().hide().next().show();
        return false;
    });
});