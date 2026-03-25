document.addEventListener("DOMContentLoaded", async () => {
	try {
		const students = await window.apiFetch("/students");

		const rows = await Promise.all(
			students.map(async (student) => {
				const results = await window.apiFetch(`/students/${student.sinhVienId}/results`);

				const validScores = results
					.filter((x) => x.diem !== null && x.diem !== undefined)
					.map((x) => Number(x.diem));

				const avg = validScores.length
					? (validScores.reduce((sum, score) => sum + score, 0) / validScores.length).toFixed(2)
					: "Chưa có";

				let rank = "Đang học";
				if (avg !== "Chưa có") {
					const avgNum = Number(avg);
					if (avgNum >= 8) rank = "Giỏi";
					else if (avgNum >= 6.5) rank = "Khá";
					else if (avgNum >= 5) rank = "Trung bình";
					else rank = "Yếu";
				}

				return [student.maSinhVien, student.hoTen, results.length, avg, rank];
			})
		);

		renderTableRows("resultsTable", rows);
	} catch (error) {
		showPageMessage(`Không tải được kết quả học tập: ${error.message}`, "danger");
	}
});
