using System.Collections.Generic;
using Code.Character.Hero;
using UnityEngine;
using Zenject;


namespace Code.Data.DataPersistence
{
    public class PersistenceObjectsCollection 
    {
        private List<IDataPersistence> dataPersistenceObjects;

        [Inject]
        private void Construct(HeroMovement heroMovement)
        {
            dataPersistenceObjects.Add(heroMovement);
        }
        
    }
}