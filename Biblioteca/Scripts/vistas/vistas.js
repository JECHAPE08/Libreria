function toggleModal(modalId) {
    const modalElement = document.getElementById(modalId);
    if (modalElement.style.display === 'flex') {
        modalElement.style.animation = 'fadeIn 0.3s reverse';
        setTimeout(() => {
            modalElement.style.display = 'none';
        }, 250);
    } else {
        modalElement.style.display = 'flex';
        modalElement.style.animation = 'fadeIn 0.3s';
    }
}

function openEditModal(id, nombre) {
    document.getElementById('editId').value = id;
    document.getElementById('editNombre').value = nombre;
    toggleModal('EditarBiblioteca');
}
function openEditClienteModal(cliente) {
    document.getElementById('EditID').value = cliente.ID;
    document.getElementById('EditNombreCliente').value = cliente.Nombre;
    document.getElementById('EditCorreo').value = cliente.Correo;
    document.getElementById('EditContrasena').value = ''; 
    document.getElementById('EditRolID').value = cliente.RolID;
    document.getElementById('EditBibliotecaID').value = cliente.BibliotecaID;

    document.getElementById('editarClienteModal').style.display = 'flex';
}
function openForm(form) {
    document.getElementById(form).style.display = "flex";
}

function closeForm(form) {
    document.getElementById(form).style.display = "none";
}
document.getElementById("formModal").addEventListener("click", function (e) {
    if (e.target === this) {
        closeForm('formModal');
    }
});

