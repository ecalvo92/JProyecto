function ConsultarNombre() {

  let identificacion = $("#Identificacion").val();
  $("#Nombre").val("");

  if (identificacion.length >= 9) {
    $.ajax({
      method: "GET",
      url: "https://apis.gometa.org/cedulas/" + identificacion,
      dataType: "json",
      success: function (data) {
        $("#Nombre").val(data.nombre);
      }
    });
  }

}