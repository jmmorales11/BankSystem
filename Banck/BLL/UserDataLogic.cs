using DAL;
using Entities;
using System;
using System.Collections.Generic;

namespace BLL
{
    public class UserDataLogic
    {
        public (bool Success, string Message, User_Data CreatedUserData) Create(User_Data users)
        {
            try
            {
                using (var r = RepositoryFactory.CreateRepository())
                {
                    var createdUserData = r.Create(users);
                    return (true, "Datos de usuario creados exitosamente", createdUserData);
                }
            }
            catch (Exception ex)
            {
                return (false, $"Error al crear datos de usuario: {ex.Message}", null);
            }
        }

        public (bool Success, string Message, List<User_Data> UserDataList) RetrieveAllUserData()
        {
            try
            {
                using (var r = RepositoryFactory.CreateRepository())
                {
                    var userDataList = r.RetrieveAll<User_Data>();
                    return (true, "Datos recuperados exitosamente", userDataList);
                }
            }
            catch (Exception ex)
            {
                return (false, $"Error al recuperar datos: {ex.Message}", null);
            }
        }

        public (bool Success, string Message, User_Data UserData) RetrieveByIdUserData(int id)
        {
            try
            {
                using (var r = RepositoryFactory.CreateRepository())
                {
                    var userData = r.Retrieve<User_Data>(p => p.user_data_id == id);
                    if (userData == null)
                    {
                        return (false, "Datos de usuario no encontrados", null);
                    }
                    return (true, "Datos de usuario encontrados", userData);
                }
            }
            catch (Exception ex)
            {
                return (false, $"Error al recuperar datos de usuario: {ex.Message}", null);
            }
        }

        public (bool Success, string Message) Delete(int id)
        {
            try
            {
                var user = RetrieveByIdUserData(id).UserData;
                if (user == null)
                {
                    return (false, "Datos de usuario no encontrados");
                }

                using (var r = RepositoryFactory.CreateRepository())
                {
                    bool deleted = r.Delete(user);
                    return deleted
                        ? (true, "Datos de usuario eliminados exitosamente")
                        : (false, "No se pudieron eliminar los datos de usuario");
                }
            }
            catch (Exception ex)
            {
                return (false, $"Error al eliminar datos de usuario: {ex.Message}");
            }
        }

        public (bool Success, string Message) UpdateUser_Data(User_Data user_Data)
        {
            try
            {
                using (var r = RepositoryFactory.CreateRepository())
                {
                    bool updated = r.Update(user_Data);
                    return updated
                        ? (true, "Datos de usuario actualizados exitosamente")
                        : (false, "No se pudieron actualizar los datos de usuario");
                }
            }
            catch (Exception ex)
            {
                return (false, $"Error al actualizar datos de usuario: {ex.Message}");
            }
        }
    }
}
