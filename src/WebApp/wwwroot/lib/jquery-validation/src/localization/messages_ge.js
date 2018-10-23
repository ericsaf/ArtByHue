/**
 * @author  @tatocaster <kutaliatato@gmail.com>
 * Translated default messages for the jQuery validation plugin.
 * Locale: GE (Georgian; )
 */
$.extend( $.validator.messages, {
	required: "  ",
	remote: " .",
	email: "   .",
	url: "   .",
	date: "   .",
	dateISO: "    ( ISO ).",
	number: "  .",
	digits: "  .",
	creditcard: "     .",
	equalTo: "   .",
	maxlength: $.validator.format( "    {0} ." ),
	minlength: $.validator.format( "  {0} ." ),
	rangelength: $.validator.format( "  {0} - {1} -  ." ),
	range: $.validator.format( " {0} - {1} - ." ),
	max: $.validator.format( "      {0} -." ),
	min: $.validator.format( "      {0} -." )
} );
