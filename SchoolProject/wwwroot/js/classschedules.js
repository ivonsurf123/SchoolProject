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
            // Obtener datos del horario
            const scheduleId = this.getAttribute('data-id');
            const courseId = this.getAttribute('data-courseid');
            const courseName = this.getAttribute('data-coursename');
            const day = this.getAttribute('data-day');
            const startTime = this.getAttribute('data-starttime');
            const endTime = this.getAttribute('data-endtime');
            const classroom = this.getAttribute('data-classroom');
            const period = this.getAttribute('data-period');
            const isActive = this.getAttribute('data-isactive') === 'true';

            // Rellenar campos del modal de edición
            document.getElementById('EditClassScheduleId').value = scheduleId;

            // Campos de solo lectura (display)
            document.getElementById('EditCourseName').value = courseName;

            // Campos ocultos (para enviar en el POST)
            document.getElementById('EditCourseIdHidden').value = courseId;

            // Campos editables
            document.getElementById('EditDay').value = day;
            document.getElementById('EditStartTime').value = startTime;
            document.getElementById('EditEndTime').value = endTime;
            document.getElementById('EditClassroom').value = classroom;
            document.getElementById('EditPeriod').value = period || '';
            document.getElementById('EditIsActive').checked = isActive;

            // Abrir modal
            const editModal = new bootstrap.Modal(document.getElementById('editScheduleModal'));
            editModal.show();
        });
    });
});

// ==================== ABRIR MODAL DE ELIMINACIÓN ====================
document.addEventListener('DOMContentLoaded', function () {
    const deleteButtons = document.querySelectorAll('.delete-btn');

    deleteButtons.forEach(button => {
        button.addEventListener('click', function () {
            const scheduleId = this.getAttribute('data-id');
            const courseName = this.getAttribute('data-course');
            const day = this.getAttribute('data-day');

            // Rellenar datos en el modal
            document.getElementById('deleteScheduleId').value = scheduleId;
            document.getElementById('scheduleInfoToDelete').textContent =
                `${courseName} - ${day}`;

            // Abrir modal
            const deleteModal = new bootstrap.Modal(document.getElementById('deleteModal'));
            deleteModal.show();
        });
    });
});

// ==================== LIMPIAR FORMULARIO AL CERRAR MODAL ====================
document.addEventListener('DOMContentLoaded', function () {
    const createModal = document.getElementById('scheduleModal');

    if (createModal) {
        createModal.addEventListener('hidden.bs.modal', function () {
            document.getElementById('scheduleForm').reset();
        });
    }
});