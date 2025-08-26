$.noConflict();

jQuery(document).ready(function ($) {

    var delay = (function () {
        var timer = 0;
        return function (callback, ms) {
            clearTimeout(timer);
            timer = setTimeout(callback, ms);
        };
    })();

	"use strict";

	[].slice.call( document.querySelectorAll( 'select.cs-select' ) ).forEach( function(el) {
		new SelectFx(el);
	} );

	jQuery('.selectpicker').selectpicker;


	$('#menuToggle').on('click', function(event) {
		$('body').toggleClass('open');
	});

	$('.search-trigger').on('click', function(event) {
		event.preventDefault();
		event.stopPropagation();
		$('.search-trigger').parent('.header-left').addClass('open');
	});

	$('.search-close').on('click', function(event) {
		event.preventDefault();
		event.stopPropagation();
		$('.search-trigger').parent('.header-left').removeClass('open');
	});

	// $('.user-area> a').on('click', function(event) {
	// 	event.preventDefault();
	// 	event.stopPropagation();
	// 	$('.user-menu').parent().removeClass('open');
	// 	$('.user-menu').parent().toggleClass('open');
	// });

	$(".autocompleteSlike").each(function () {
	    var komponenta, poljeVrijednost, poljeNaziv, lista, pojam, znakova, poljeSlika;
	    komponenta = $(this);
	    poljeVrijednost = komponenta.find(".slikaid");
	    //poljeSlika = komponenta.find(".slika");
	    poljeNaziv = komponenta.find(".naziv");
	    lista = komponenta.find(".listaSlika");
	    //Ne propagiraj klik dogaðaj
	    komponenta.click(function (e) {
	        e.stopPropagation();
	    });
	    //Uzmi podatke
	    poljeNaziv.keyup(function () {
	        delay(function () {
	            pojam = $.trim(poljeNaziv.val());
	            znakova = pojam.length;
	            if (znakova > 0) {
	                $.get(komponenta.data("url"), { pojam: pojam }, function (podaci) {
	                    lista.html(podaci).show();
	                });
	            } else {
	                lista.hide();
	                poljeVrijednost.val("0");
	            }
	        }, 500);
	    });
	    //Prikaži postojeæe podatke
	    poljeNaziv.click(function () {
	        $(".autocompleteSlike .listaSlika").hide();
	        if (znakova > 0) {
	            lista.show();
	        }
	    });
	    //Odabir retka
	    lista.on("click", ".redak", function () {
	        poljeNaziv.val($(this).data("naziv"));
	        poljeVrijednost.val($(this).data("slikaid"));
	        artikalid = komponenta.find(".ArtikalID").val();
	        $.post("/CMS/Ajax/PridruziSlikuArtiklu.aspx", {
	            slika: poljeNaziv.val(),
	            artikalid: artikalid
	        }, function () {
	            location.reload();
	        });
	        lista.hide();
	    });
	});


});

jQuery(document).click(function () {
    jQuery(".autocompleteSlike .listaSlika").hide();
});