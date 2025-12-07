// ==================== FECHA ACTUAL ====================
document.addEventListener('DOMContentLoaded', function () {
    const dateElement = document.getElementById('currentDate');
    if (dateElement) {
        const today = new Date();
        const options = { weekday: 'long', year: 'numeric', month: 'long', day: 'numeric' };
        dateElement.textContent = today.toLocaleDateString('es-ES', options);
    }
});

// ==================== ABRIR MODAL DE EDICIÓN ====================
document.addEventListener('DOMContentLoaded', function () {
    const editButtons = document.querySelectorAll('.edit-btn');

    editButtons.forEach(button => {
        button.addEventListener('click', function () {
            // Obtener datos del profesor
            const professorId = this.getAttribute('data-id');
            const code = this.getAttribute('data-code');
            const name = this.getAttribute('data-name');
            const lastname = this.getAttribute('data-lastname');
            const email = this.getAttribute('data-email');
            const phone = this.getAttribute('data-phone');
            const department = this.getAttribute('data-department');
            const hiringDate = this.getAttribute('data-hiringdate');
            const isActive = this.getAttribute('data-isactive') === 'true';

            // Rellenar campos del modal de edición
            document.getElementById('EditProfessorId').value = professorId;

            // Campos de solo lectura (display)
            document.getElementById('EditCode').value = code;
            document.getElementById('EditName').value = name;
            document.getElementById('EditLastName').value = lastname;
            document.getElementById('EditEmail').value = email;
            document.getElementById('EditHiringDate').value = hiringDate;

            // Campos ocultos (para enviar en el POST)
            document.getElementById('EditCodeHidden').value = code;
            document.getElementById('EditNameHidden').value = name;
            document.getElementById('EditLastNameHidden').value = lastname;
            document.getElementById('EditEmailHidden').value = email;
            document.getElementById('EditHiringDateHidden').value = hiringDate;

            // Campos editables
            document.getElementById('EditPhone').value = phone;
            document.getElementById('EditDepartment').value = department;
            document.getElementById('EditIsActive').checked = isActive;

            // Abrir modal
            const editModal = new bootstrap.Modal(document.getElementById('editProfessorModal'));
            editModal.show();
        });
    });
});

// ==================== ABRIR MODAL DE ELIMINACIÓN ====================
document.addEventListener('DOMContentLoaded', function () {
    const deleteButtons = document.querySelectorAll('.delete-btn');

    deleteButtons.forEach(button => {
        button.addEventListener('click', function () {
            const professorId = this.getAttribute('data-id');
            const professorName = this.getAttribute('data-name');

            // Rellenar datos en el modal
            document.getElementById('deleteProfessorId').value = professorId;
            document.getElementById('professorNameToDelete').textContent = professorName;

            // Abrir modal
            const deleteModal = new bootstrap.Modal(document.getElementById('deleteModal'));
            deleteModal.show();
        });
    });
});

// ==================== LIMPIAR FORMULARIO AL CERRAR MODAL ====================
document.addEventListener('DOMContentLoaded', function () {
    const createModal = document.getElementById('professorModal');

    if (createModal) {
        createModal.addEventListener('hidden.bs.modal', function () {
            document.getElementById('professorForm').reset();
        });
    }
});