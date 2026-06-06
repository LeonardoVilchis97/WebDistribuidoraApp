let editarProducto = (function () {

    function validarPrecioVenta() {
        $("#precioVenta").removeClass("is-invalid");
        let precioVenta = parseFloat($("#precioVenta").val());
        if (isNaN(precioVenta) || precioVenta <= 0) {
            $("#precioVenta").addClass("is-invalid");
            mostrarToast("El precio de venta debe ser mayor a 0.", "danger");
        }
    }

    function mostrarToast(mensaje, tipo = "danger") {
        const toastElement = document.getElementById("toastMensaje");
        const toastTexto = document.getElementById("toastTexto");
        toastTexto.textContent = mensaje;
        toastElement.classList.remove(
            "text-bg-danger",
            "text-bg-success",
            "text-bg-warning",
            "text-bg-info"
        );
        toastElement.classList.add(`text-bg-${tipo}`);
        const toast = bootstrap.Toast.getOrCreateInstance(toastElement, { delay: 3000 });
        toast.show();
    }

    function inicializarFormularioModal() {
        // ✅ off() elimina handlers previos antes de registrar uno nuevo
        $(document).off('submit', '#modalAgregarProveedorContent form')
            .on('submit', '#modalAgregarProveedorContent form', function (e) {
                e.preventDefault();
                var form = $(this);
                var url = form.attr('action');
                var data = form.serialize();

                $.post(url, data)
                    .done(function (response) {
                        if ($(response).find("form").length > 0) {
                            $('#modalAgregarProveedorContent').html(response);
                        } else {
                            $('#modalAgregarProveedor').modal('hide');
                            location.reload();
                        }
                    })
                    .fail(function () {
                        alert('Error al guardar. Intenta de nuevo.');
                    });
            });
    }

    function inicializarModal() {
        $(document).off('show.bs.modal', '#modalAgregarProveedor')
            .on('show.bs.modal', '#modalAgregarProveedor', function (event) {
                var button = $(event.relatedTarget);
                var url = button.data('url');

                if (!url) {
                    console.log("URL inválida");
                    return;
                }

                $('#modalAgregarProveedorContent').html(
                    '<div class="modal-body text-center">Cargando...</div>'
                );

                $.get(url)
                    .done(function (data) {
                        $('#modalAgregarProveedorContent').html(data);
                    })
                    .fail(function () {
                        $('#modalAgregarProveedorContent').html(
                            '<div class="modal-body text-danger"><h5>Error al cargar el modal</h5></div>'
                        );
                    });
            });

        
        $(document).off('hidden.bs.modal', '#modalAgregarProveedor')
            .on('hidden.bs.modal', '#modalAgregarProveedor', function () {
                $('#modalAgregarProveedorContent').html(
                    '<div class="modal-body text-center">Cargando...</div>'
                );
            });
    }


    return {
        validarPrecioVenta: function () { validarPrecioVenta(); },
        mostrarToast: function (mensaje, tipo) { mostrarToast(mensaje, tipo); },
        inicializarModal: function () { inicializarModal(); },
        inicializarFormularioModal: function () { inicializarFormularioModal(); }
    }

})();
