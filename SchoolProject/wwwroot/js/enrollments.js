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
            // Obtener datos de la inscripción
            const enrollmentId = this.getAttribute('data-id');
            const studentId = this.getAttribute('data-studentid');
            const studentName = this.getAttribute('data-studentname');
            const courseId = this.getAttribute('data-courseid');
            const courseName = this.getAttribute('data-coursename');
            const period = this.getAttribute('data-period');
            const isActive = this.getAttribute('data-isactive') === 'true';

            // Rellenar campos del modal de edición
            document.getElementById('EditEnrollmentId').value = enrollmentId;

            // Campos de solo lectura (display)
            document.getElementById('EditStudentName').value = studentName;
            document.getElementById('EditCourseName').value = courseName;

            // Campos ocultos (para enviar en el POST)
            document.getElementById('EditStudentIdHidden').value = studentId;
            document.getElementById('EditCourseIdHidden').value = courseId;

            // Campos editables
            document.getElementById('EditPeriod').value = period;
            document.getElementById('EditIsActive').checked = isActive;

            // Abrir modal
            const editModal = new bootstrap.Modal(document.getElementById('editEnrollmentModal'));
            editModal.show();
        });
    });
});

// ==================== ABRIR MODAL DE CANCELACIÓN ====================
document.addEventListener('DOMContentLoaded', function () {
    const deleteButtons = document.querySelectorAll('.delete-btn');

    deleteButtons.forEach(button => {
        button.addEventListener('click', function () {
            const enrollmentId = this.getAttribute('data-id');
            const studentName = this.getAttribute('data-student');
            const courseName = this.getAttribute('data-course');

            // Rellenar datos en el modal
            document.getElementById('deleteEnrollmentId').value = enrollmentId;
            document.getElementById('enrollmentInfoToDelete').textContent =
                `${studentName} en ${courseName}`;

            // Abrir modal
            const deleteModal = new bootstrap.Modal(document.getElementById('deleteModal'));
            deleteModal.show();
        });
    });
});

// ==================== LIMPIAR FORMULARIO AL CERRAR MODAL ====================
document.addEventListener('DOMContentLoaded', function () {
    const createModal = document.getElementById('enrollmentModal');

    if (createModal) {
        createModal.addEventListener('hidden.bs.modal', function () {
            document.getElementById('enrollmentForm').reset();
        });
    }
});