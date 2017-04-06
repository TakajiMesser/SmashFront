using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmashFront.Models.Characters
{
    public enum MoveType
    {
        NeutralAttack,
        ForwardTilt,
        UpTilt,
        DownTilt,
        DashAttack,
        ForwardSmash,
        UpSmash,
        DownSmash,
        NeutralAerial,
        ForwardAerial,
        BackAerial,
        UpAerial,
        DownAerial,
        Grab,
        Pummel,
        ForwardThrow,
        BackThrow,
        UpThrow,
        DownThrow,
        FloorAttackFront,
        FloorAttackBack,
        EdgeAttackFast,
        EdgeAttackSlow,
        NeutralSpecial,
        SideSpecial,
        UpSpecial,
        DownSpecial
    };

    public class Move
    {
        MoveType Type { get; set; }
        String Name { get; set; }
        String Description { get; set; }
        List<Int32> HitDamage { get; set; }

        public Move(MoveType type, String name, String description, List<Int32> hitDamage)
        {
            Type = type;
            Name = name;
            Description = description;
            HitDamage.AddRange(hitDamage);
        }
    }
}
