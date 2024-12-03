using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace DVService
{
    public class DeliveryService : IDeliveryService
    {
        // Chaîne de connexion pour la base de données bdRestaurant
        private readonly string connectionString = "Server=localhost;Database=bdRestaurant;User ID=root;Password=;";

        public DeliveryService()
        {
            CreerTableSiInexistante();
        }

        // Méthode pour créer la table 'deliveries' si elle n'existe pas
        private void CreerTableSiInexistante()
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string creationTableQuery = @"
                        CREATE TABLE IF NOT EXISTS deliveries (
                            Id BIGINT AUTO_INCREMENT PRIMARY KEY,
                            OrderId BIGINT NOT NULL,
                            DeliveryAddress VARCHAR(255) NOT NULL,
                            Contact VARCHAR(100) NOT NULL,
                            EstimatedTime DATETIME NOT NULL,
                            Status ENUM('Scheduled', 'In Transit', 'Delivered', 'Cancelled') NOT NULL
                        );";

                    using (MySqlCommand command = new MySqlCommand(creationTableQuery, conn))
                    {
                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erreur lors de la création de la table : {ex.Message}");
                }
            }
        }

        // Récupérer les détails d'une livraison par ID
        public Delivery GetDeliveryDetails(long id)
        {
            Delivery delivery = null;
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                var command = new MySqlCommand("SELECT * FROM deliveries WHERE Id = @Id", conn);
                command.Parameters.AddWithValue("@Id", id);

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        delivery = new Delivery
                        {
                            Id = Convert.ToInt64(reader["Id"]),
                            OrderId = Convert.ToInt64(reader["OrderId"]),
                            DeliveryAddress = reader["DeliveryAddress"].ToString(),
                            Contact = reader["Contact"].ToString(),
                            EstimatedTime = reader["EstimatedTime"].ToString(),
                            Status = reader["Status"].ToString()
                        };
                    }
                }
            }

            return delivery;
        }

        // Récupérer une liste de livraisons par OrderId
        public List<Delivery> GetDeliveriesByOrderId(long orderId)
        {
            var deliveries = new List<Delivery>();

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                var command = new MySqlCommand("SELECT * FROM deliveries WHERE OrderId = @OrderId", conn);
                command.Parameters.AddWithValue("@OrderId", orderId);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        deliveries.Add(new Delivery
                        {
                            Id = Convert.ToInt64(reader["Id"]),
                            OrderId = Convert.ToInt64(reader["OrderId"]),
                            DeliveryAddress = reader["DeliveryAddress"].ToString(),
                            Contact = reader["Contact"].ToString(),
                            EstimatedTime = reader["EstimatedTime"].ToString(),
                            Status = reader["Status"].ToString()
                        });
                    }
                }
            }

            return deliveries;
        }

        // Mettre à jour le statut d'une livraison
        public string UpdateDeliveryStatus(long id, string status)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                var command = new MySqlCommand("UPDATE deliveries SET Status = @Status WHERE Id = @Id", conn);
                command.Parameters.AddWithValue("@Status", status);
                command.Parameters.AddWithValue("@Id", id);

                int rowsAffected = command.ExecuteNonQuery();

                return rowsAffected > 0 ? "Delivery status updated successfully" : "Error updating delivery status";
            }
        }

        // Ajouter une nouvelle livraison
        public string AddDelivery(Delivery newDelivery)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                var command = new MySqlCommand(@"
                    INSERT INTO deliveries (OrderId, DeliveryAddress, Contact, EstimatedTime, Status)
                    VALUES (@OrderId, @DeliveryAddress, @Contact, @EstimatedTime, @Status)", conn);

                // Ajout des paramètres pour éviter les injections SQL
                command.Parameters.AddWithValue("@OrderId", newDelivery.OrderId);
                command.Parameters.AddWithValue("@DeliveryAddress", newDelivery.DeliveryAddress);
                command.Parameters.AddWithValue("@Contact", newDelivery.Contact);
                command.Parameters.AddWithValue("@EstimatedTime", newDelivery.EstimatedTime);
                command.Parameters.AddWithValue("@Status", newDelivery.Status);

                // Exécution de la commande
                int rowsAffected = command.ExecuteNonQuery();

                // Retourner un message de succès ou d'erreur
                return rowsAffected > 0 ? "Delivery added successfully" : "Error adding delivery";
            }
        }
    }
}
