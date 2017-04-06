using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmashFront.Models.Characters.Fox
{
    public class Fox : BaseCharacter
    {
        public Fox()
        {
            Name = "Fox";

            Moveset.Add(new Move(MoveType.NeutralAttack, "", "", new List<Int32>() { 4, 4, 1 }));
            Moveset.Add(new Move(MoveType.ForwardTilt, "", "", new List<Int32>() { 4, 4, 1 }));
            Moveset.Add(new Move(MoveType.UpTilt, "", "", new List<Int32>() { 4, 4, 1 }));
            Moveset.Add(new Move(MoveType.DownTilt, "", "", new List<Int32>() { 4, 4, 1 }));
            Moveset.Add(new Move(MoveType.DashAttack, "", "", new List<Int32>() { 4, 4, 1 }));
            Moveset.Add(new Move(MoveType.ForwardSmash, "", "", new List<Int32>() { 4, 4, 1 }));
            Moveset.Add(new Move(MoveType.UpSmash, "Flip Kick", "", new List<Int32>() { 4, 4, 1 }));
            Moveset.Add(new Move(MoveType.DownSmash, "", "", new List<Int32>() { 4, 4, 1 }));
            Moveset.Add(new Move(MoveType.NeutralAerial, "", "", new List<Int32>() { 4, 4, 1 }));
            Moveset.Add(new Move(MoveType.ForwardAerial, "", "", new List<Int32>() { 4, 4, 1 }));
            Moveset.Add(new Move(MoveType.BackAerial, "", "", new List<Int32>() { 4, 4, 1 }));
            Moveset.Add(new Move(MoveType.UpAerial, "", "", new List<Int32>() { 4, 4, 1 }));
            Moveset.Add(new Move(MoveType.DownAerial, "", "", new List<Int32>() { 4, 4, 1 }));
            Moveset.Add(new Move(MoveType.Grab, "", "", new List<Int32>() { 4, 4, 1 }));
            Moveset.Add(new Move(MoveType.Pummel, "", "", new List<Int32>() { 4, 4, 1 }));
            Moveset.Add(new Move(MoveType.ForwardThrow, "", "", new List<Int32>() { 4, 4, 1 }));
            Moveset.Add(new Move(MoveType.BackThrow, "", "", new List<Int32>() { 4, 4, 1 }));
            Moveset.Add(new Move(MoveType.UpThrow, "", "", new List<Int32>() { 4, 4, 1 }));
            Moveset.Add(new Move(MoveType.DownThrow, "", "", new List<Int32>() { 4, 4, 1 }));
            Moveset.Add(new Move(MoveType.FloorAttackFront, "", "", new List<Int32>() { 4, 4, 1 }));
            Moveset.Add(new Move(MoveType.FloorAttackBack, "", "", new List<Int32>() { 4, 4, 1 }));
            Moveset.Add(new Move(MoveType.EdgeAttackFast, "", "", new List<Int32>() { 4, 4, 1 }));
            Moveset.Add(new Move(MoveType.EdgeAttackSlow, "", "", new List<Int32>() { 4, 4, 1 }));
            Moveset.Add(new Move(MoveType.NeutralSpecial, "Blaster", "", new List<Int32>() { 4, 4, 1 }));
            Moveset.Add(new Move(MoveType.SideSpecial, "Fox Illusion", "", new List<Int32>() { 4, 4, 1 }));
            Moveset.Add(new Move(MoveType.UpSpecial, "Fire Fox", "", new List<Int32>() { 4, 4, 1 }));
            Moveset.Add(new Move(MoveType.DownSpecial, "Reflector", "", new List<Int32>() { 4, 4, 1 }));
        }
    }
}
