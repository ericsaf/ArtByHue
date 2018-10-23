/*
 * Translated default messages for the jQuery validation plugin.
 * Locale: PL (Polish; jzyk polski, polszczyzna)
 */
$.extend( $.validator.messages, {
	required: "To pole jest wymagane.",
	remote: "Prosz o wypenienie tego pola.",
	email: "Prosz o podanie prawidowego adresu email.",
	url: "Prosz o podanie prawidowego URL.",
	date: "Prosz o podanie prawidowej daty.",
	dateISO: "Prosz o podanie prawidowej daty (ISO).",
	number: "Prosz o podanie prawidowej liczby.",
	digits: "Prosz o podanie samych cyfr.",
	creditcard: "Prosz o podanie prawidowej karty kredytowej.",
	equalTo: "Prosz o podanie tej samej wartoci ponownie.",
	extension: "Prosz o podanie wartoci z prawidowym rozszerzeniem.",
	maxlength: $.validator.format( "Prosz o podanie nie wicej ni {0} znakw." ),
	minlength: $.validator.format( "Prosz o podanie przynajmniej {0} znakw." ),
	rangelength: $.validator.format( "Prosz o podanie wartoci o dugoci od {0} do {1} znakw." ),
	range: $.validator.format( "Prosz o podanie wartoci z przedziau od {0} do {1}." ),
	max: $.validator.format( "Prosz o podanie wartoci mniejszej bd rwnej {0}." ),
	min: $.validator.format( "Prosz o podanie wartoci wikszej bd rwnej {0}." ),
	pattern: $.validator.format( "Pole zawiera niedozwolone znaki." )
} );
