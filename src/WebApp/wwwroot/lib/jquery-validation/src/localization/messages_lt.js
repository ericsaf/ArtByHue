/*
 * Translated default messages for the jQuery validation plugin.
 * Locale: LT (Lithuanian; lietuvi kalba)
 */
$.extend( $.validator.messages, {
	required: "is laukas yra privalomas.",
	remote: "Praau pataisyti  lauk.",
	email: "Praau vesti teising elektroninio pato adres.",
	url: "Praau vesti teising URL.",
	date: "Praau vesti teising dat.",
	dateISO: "Praau vesti teising dat (ISO).",
	number: "Praau vesti teising skaii.",
	digits: "Praau naudoti tik skaitmenis.",
	creditcard: "Praau vesti teising kreditins kortels numer.",
	equalTo: "Praau vest t pai reikm dar kart.",
	extension: "Praau vesti reikm su teisingu pltiniu.",
	maxlength: $.validator.format( "Praau vesti ne daugiau kaip {0} simboli." ),
	minlength: $.validator.format( "Praau vesti bent {0} simbolius." ),
	rangelength: $.validator.format( "Praau vesti reikmes, kuri ilgis nuo {0} iki {1} simboli." ),
	range: $.validator.format( "Praau vesti reikm intervale nuo {0} iki {1}." ),
	max: $.validator.format( "Praau vesti reikm maesn arba lygi {0}." ),
	min: $.validator.format( "Praau vesti reikm didesn arba lygi {0}." )
} );
