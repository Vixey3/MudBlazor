using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DiplomskiBlazor.Server.Migrations
{
    public partial class TeretanaMigracija : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DeloviTela",
                columns: table => new
                {
                    deoTelaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nazivTela = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeloviTela", x => x.deoTelaId);
                });

            migrationBuilder.CreateTable(
                name: "Korisnici",
                columns: table => new
                {
                    korisnikId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    korisnickoIme = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    lozinka = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    prezime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    telefon = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    pol = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Korisnici", x => x.korisnikId);
                });

            migrationBuilder.CreateTable(
                name: "Workouts",
                columns: table => new
                {
                    workoutId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nazivWorkouta = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    opisWorkouta = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Workouts", x => x.workoutId);
                });

            migrationBuilder.CreateTable(
                name: "Vezbe",
                columns: table => new
                {
                    vezbaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nazivVezbe = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    opisVezbe = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    brojSerijaPonavljanja = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    deoTelaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vezbe", x => x.vezbaId);
                    table.ForeignKey(
                        name: "FK_Vezbe_DeloviTela_deoTelaId",
                        column: x => x.deoTelaId,
                        principalTable: "DeloviTela",
                        principalColumn: "deoTelaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Statistike",
                columns: table => new
                {
                    statistikaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    datum = table.Column<DateTime>(type: "datetime2", nullable: false),
                    visina = table.Column<int>(type: "int", nullable: false),
                    tezina = table.Column<double>(type: "float", nullable: false),
                    obimStruka = table.Column<double>(type: "float", nullable: false),
                    obimGrudi = table.Column<double>(type: "float", nullable: false),
                    obimKukova = table.Column<double>(type: "float", nullable: false),
                    korisnikId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Statistike", x => x.statistikaId);
                    table.ForeignKey(
                        name: "FK_Statistike_Korisnici_korisnikId",
                        column: x => x.korisnikId,
                        principalTable: "Korisnici",
                        principalColumn: "korisnikId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KorisnikWorkout",
                columns: table => new
                {
                    korisnicikorisnikId = table.Column<int>(type: "int", nullable: false),
                    workoutsworkoutId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KorisnikWorkout", x => new { x.korisnicikorisnikId, x.workoutsworkoutId });
                    table.ForeignKey(
                        name: "FK_KorisnikWorkout_Korisnici_korisnicikorisnikId",
                        column: x => x.korisnicikorisnikId,
                        principalTable: "Korisnici",
                        principalColumn: "korisnikId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_KorisnikWorkout_Workouts_workoutsworkoutId",
                        column: x => x.workoutsworkoutId,
                        principalTable: "Workouts",
                        principalColumn: "workoutId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VezbaWorkout",
                columns: table => new
                {
                    vezbevezbaId = table.Column<int>(type: "int", nullable: false),
                    workoutsworkoutId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VezbaWorkout", x => new { x.vezbevezbaId, x.workoutsworkoutId });
                    table.ForeignKey(
                        name: "FK_VezbaWorkout_Vezbe_vezbevezbaId",
                        column: x => x.vezbevezbaId,
                        principalTable: "Vezbe",
                        principalColumn: "vezbaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VezbaWorkout_Workouts_workoutsworkoutId",
                        column: x => x.workoutsworkoutId,
                        principalTable: "Workouts",
                        principalColumn: "workoutId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_KorisnikWorkout_workoutsworkoutId",
                table: "KorisnikWorkout",
                column: "workoutsworkoutId");

            migrationBuilder.CreateIndex(
                name: "IX_Statistike_korisnikId",
                table: "Statistike",
                column: "korisnikId");

            migrationBuilder.CreateIndex(
                name: "IX_VezbaWorkout_workoutsworkoutId",
                table: "VezbaWorkout",
                column: "workoutsworkoutId");

            migrationBuilder.CreateIndex(
                name: "IX_Vezbe_deoTelaId",
                table: "Vezbe",
                column: "deoTelaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KorisnikWorkout");

            migrationBuilder.DropTable(
                name: "Statistike");

            migrationBuilder.DropTable(
                name: "VezbaWorkout");

            migrationBuilder.DropTable(
                name: "Korisnici");

            migrationBuilder.DropTable(
                name: "Vezbe");

            migrationBuilder.DropTable(
                name: "Workouts");

            migrationBuilder.DropTable(
                name: "DeloviTela");
        }
    }
}
