$(function () {
    var bookSlider = $('#bookRange');
    bookSlider.on('change', function () {
        goToPage(bookSlider.val());
    });

    var synonym = $('.book-container').data('synonym');
    var firstPage = $('.book-container').data('first-page');
    var lastPage = $('.book-container').data('last-page');

    function goToPage(page) {
        window.history.pushState({ page: page }, '', "/books/" + synonym + '/' + page);
        loadPage(page);
    }

    function loadPage(page) {
        page = +page;
        $.ajax({
            url: serverOptions.apiUrl + '/api/public/GetBookPageByNumber',
            type: 'POST',
            data: JSON.stringify({ synonym: synonym, number: page }),
            contentType: 'application/json',
            dataType: 'json',
            success: function (data) {
                bookSlider.val(page);
                $('.book-page img').attr('src', data.fileName);
                $('.book-page a').data('page', page < lastPage ? page + 1 : null);
                $('.book-page a').attr('href', page < lastPage ? '/books/' + synonym + '/' + (page + 1) : null);
                $('.book-controls .book-prev').data('page', page > firstPage ? page - 1 : null);
                $('.book-controls .book-prev').attr('href', page > firstPage ? '/books/' + synonym + '/' + (page - 1) : null);
                $('.book-controls .book-next').data('page', page < lastPage ? page + 1 : null);
                $('.book-controls .book-next').attr('href', page < lastPage ? '/books/' + synonym + '/' + (page + 1) : null);
            }
        });
    }

    $('.book-controls .book-prev, .book-controls .book-next').click(function () {
        var page = $(this).data('page');
        if (page) {
            goToPage(page);
        }
        return false;
    });

    $('.book-labels a, .book-page a').click(function () {
        var page = $(this).data('page');
        if (page) {
            goToPage(page);
        }
        return false;
    });

    window.onpopstate = function (event) {
        if (event.state && event.state.page) {
            loadPage(event.state.page);
        }
    };
});