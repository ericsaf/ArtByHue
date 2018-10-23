/*
 * Translated default messages for the jQuery validation plugin.
 * Locale: CA (Catalan; catal)
 */
$.extend( $.validator.messages, {
	required: "Aquest camp s obligatori.",
	remote: "Si us plau, omple aquest camp.",
	email: "Si us plau, escriu una adrea de correu-e vlida",
	url: "Si us plau, escriu una URL vlida.",
	date: "Si us plau, escriu una data vlida.",
	dateISO: "Si us plau, escriu una data (ISO) vlida.",
	number: "Si us plau, escriu un nmero enter vlid.",
	digits: "Si us plau, escriu noms dgits.",
	creditcard: "Si us plau, escriu un nmero de tarjeta vlid.",
	equalTo: "Si us plau, escriu el mateix valor de nou.",
	extension: "Si us plau, escriu un valor amb una extensi acceptada.",
	maxlength: $.validator.format( "Si us plau, no escriguis ms de {0} caracters." ),
	minlength: $.validator.format( "Si us plau, no escriguis menys de {0} caracters." ),
	rangelength: $.validator.format( "Si us plau, escriu un valor entre {0} i {1} caracters." ),
	range: $.validator.format( "Si us plau, escriu un valor entre {0} i {1}." ),
	max: $.validator.format( "Si us plau, escriu un valor menor o igual a {0}." ),
	min: $.validator.format( "Si us plau, escriu un valor major o igual a {0}." )
} );
