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
            // Obtener datos del curso
            const courseId = this.getAttribute('data-id');
            const code = this.getAttribute('data-code');
            const name = this.getAttribute('data-name');
            const description = this.getAttribute('data-description');
            const credits = this.getAttribute('data-credits');
            const level = this.getAttribute('data-level');
            const grade = this.getAttribute('data-grade');
            const professorId = this.getAttribute('data-professorid');
            const isActive = this.getAttribute('data-isactive') === 'true';

            // Rellenar campos del modal de edición
            document.getElementById('EditCourseId').value = courseId;

            // Campos de solo lectura (display)
            document.getElementById('EditCode').value = code;
            document.getElementById('EditName').value = name;

            // Campos ocultos (para enviar en el POST)
            document.getElementById('EditCodeHidden').value = code;
            document.getElementById('EditNameHidden').value = name;

            // Campos editables
            document.getElementById('EditDescription').value = description;
            document.getElementById('EditCredits').value = credits;
            document.getElementById('EditLevel').value = level;
            document.getElementById('EditGrade').value = grade;
            document.getElementById('EditProfessorId').value = professorId;
            document.getElementById('EditIsActive').checked = isActive;

            // Abrir modal
            const editModal = new bootstrap.Modal(document.getElementById('editCourseModal'));
            editModal.show();
        });
    });
});

// ==================== ABRIR MODAL DE ELIMINACIÓN ====================
document.addEventListener('DOMContentLoaded', function () {
    const deleteButtons = document.querySelectorAll('.delete-btn');

    deleteButtons.forEach(button => {
        button.addEventListener('click', function () {
            const courseId = this.getAttribute('data-id');
            const courseName = this.getAttribute('data-name');

            // Rellenar datos en el modal
            document.getElementById('deleteCourseId').value = courseId;
            document.getElementById('courseNameToDelete').textContent = courseName;

            // Abrir modal
            const deleteModal = new bootstrap.Modal(document.getElementById('deleteModal'));
            deleteModal.show();
        });
    });
});

// ==================== LIMPIAR FORMULARIO AL CERRAR MODAL ====================
document.addEventListener('DOMContentLoaded', function () {
    const createModal = document.getElementById('courseModal');

    if (createModal) {
        createModal.addEventListener('hidden.bs.modal', function () {
            document.getElementById('courseForm').reset();
        });
    }
});